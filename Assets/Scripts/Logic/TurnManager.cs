using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;


// this class will take care of switching turns and counting down time until the turn expires
public class TurnManager : MonoBehaviour {

    // for Singleton Pattern
    public static TurnManager Instance;

    // PRIVATE FIELDS
    // reference to a timer to measure 
    public RopeTimer timer;

    public bool endTurnForced = false;

    int roundCounter;

    public delegate void ResetRound();    
    public event ResetRound e_ResetRound; 

     public delegate void EndOfRound();    
    public event EndOfRound e_EndOfRound; 

    public delegate void GameOver();    
    public event GameOver e_GameOver; 

    private Player firstPlayer;

    bool isRoundOver;


    // PROPERTIES
    private Player _whoseTurn;
    public Player whoseTurn
    {
        get
        {
            return _whoseTurn;
        }

        set
        {
            _whoseTurn = value;
            timer.StartTimer();

            GlobalSettings.Instance.EnableEndTurnButtonOnStart(_whoseTurn);

            TurnMaker tm = whoseTurn.GetComponent<TurnMaker>();
            // player`s method OnTurnStart() will be called in tm.OnTurnStart();
            tm.OnTurnStart();

            if (tm is PlayerTurnMaker)
            {
            //DS    whoseTurn.HighlightPlayableCards();
            }
                
        }
    }

    //Extra Turn
    private int turnCounter = 0;
    public int TurnCounter
    {
        get{return turnCounter;}
        set
        {
            
            if(value>=1)
            turnCounter=1;

            if(value<=0)
            turnCounter=0;
        }
    }


    // METHODS
    void Awake()
    {
        Instance = this;
        timer = GetComponent<RopeTimer>();

        roundCounter = 1;
        
    }

    void Start()
    {
        //OnGameStart();
        StartCoroutine(OnGameStart());
    }



    //public void OnGameStart()
    IEnumerator OnGameStart()
    {

        //selector of TurnMaker for Game Mode, if PvAI or AIvAI
        if (BattleStartInfo.gameMode.ToString() != null)
        {
            switch (BattleStartInfo.gameMode)
            {
                case GameMode.PvAI:
                    TurnMaker tm = GlobalSettings.Instance.TopPlayer.GetComponent<TurnMaker>();
                    Destroy(tm);
                    GlobalSettings.Instance.TopPlayer.gameObject.AddComponent<AITurnMaker>();
                    break;
                
                case GameMode.AIvAI:
                    TurnMaker tm1 = GlobalSettings.Instance.TopPlayer.GetComponent<TurnMaker>();
                    Destroy(tm1);
                    GlobalSettings.Instance.TopPlayer.gameObject.AddComponent<AITurnMaker>();
                    TurnMaker tm2 = GlobalSettings.Instance.LowPlayer.GetComponent<TurnMaker>();
                    Destroy(tm2);
                    GlobalSettings.Instance.LowPlayer.gameObject.AddComponent<AITurnMaker>();
                    break;
            }
        }

        //clear data
        CardLogic.CardsCreatedThisGame.Clear();
        CreatureLogic.CreaturesCreatedThisGame.Clear();



        //DS - use this sequence later on for any Start of Battle loading/animation effects

        foreach (Player p in Player.Players)
        {

            p.TransmitInfoAboutPlayerToVisual();
            p.PArea.PDeck.CardsInDeck = p.deck.cards.Count;
 
        }

        Player whoGoesFirst;
        Player whoGoesSecond;

        //force a side to go first
        if (Tutorial1.Instance != null && Tutorial1.Instance.tutorialState != TutorialState.COMPLETED)
        {
            whoGoesFirst = Player.Players[1];
            whoGoesSecond = whoGoesFirst.otherPlayer;
        }

        //randomize who will go first
        else
        {
            int rnd = Random.Range(0,2);  // 2 is exclusive boundary
            whoGoesFirst = Player.Players[rnd];
            whoGoesSecond = whoGoesFirst.otherPlayer;
        }

        //SAVE Info on who's the original first player
        firstPlayer = whoGoesFirst;
    
        yield return null;

        //draw from deck and put creatures to table - 2nd player
        int cardCountSecond = whoGoesSecond.deck.cards.Count;
        for (int i = 0; i < cardCountSecond; i++)
        {            
            whoGoesSecond.DrawACard(true);
            yield return null;
            whoGoesSecond.PlayACreatureFromHand(whoGoesSecond.hand.CardsInHand[0], 0);        
        }

        yield return null;

        //draw from deck and put creatures to table - 1st player
        int cardCountFirst = whoGoesFirst.deck.cards.Count;
        for (int i = 0; i < cardCountFirst; i++)
        {            
            whoGoesFirst.DrawACard(true);
            yield return null;
            whoGoesFirst.PlayACreatureFromHand(whoGoesFirst.hand.CardsInHand[0], 0);
        }

        yield return null;

        //initialize creature effectes
        /* transfer this to CL
        foreach (Player player in Player.Players)
        {                    
            
            foreach(CreatureLogic cl in player.AllyList())
            {
                foreach (CreatureEffect ce in cl.creatureEffects)
                {
                    ce.RegisterCooldown();
                    ce.RegisterEventEffect();

                    //DS Test: 24 Nov 2019. Initialize Ultimate Skill CD to 1.  
                    if(ce.skillType == SkillType.Ultimate)
                    {
                        ce.remainingCooldown = 1;
                        new UpdateCooldownCommand(ce.abilityCard, ce.remainingCooldown, ce.creatureEffectCooldown).AddToQueue();
                    }
                    
                }
                cl.OnTurnStart();
            }
        }
        */
        //DS
        //Display skills panel of player only

        if(Tutorial1.Instance == null || Tutorial1.Instance.tutorialState == TutorialState.COMPLETED)
        {
            if(whoGoesFirst.PArea.owner == AreaPosition.Low)
            new ShowSkillsPanelCommand(whoGoesFirst).AddToQueue();
            else
            new ShowSkillsPanelCommand(whoGoesSecond).AddToQueue();

            //start the turn for 1st player
            new StartATurnCommand(whoGoesFirst).AddToQueue();
        }

        yield return null;
        StopCoroutine(OnGameStart());

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EndTurn();
    }

    public void EndTurn()
    {           
        StartCoroutine(TurnEndCoroutine());        
    }

    public void EndTurnForced()
    {
        endTurnForced = true;         
        StartCoroutine(TimerExpired());        
    }

    IEnumerator TimerExpired()
    {
        yield return StartCoroutine(PenalizePlayer());
        yield return StartCoroutine(TurnEndCoroutine());
    }

    IEnumerator PenalizePlayer()
    {
        if(CanACreatureAttack())
        {
            int i = Random.Range(0,whoseTurn.AllyList().Count);
            while (whoseTurn.AllyList()[i].isDead || whoseTurn.AllyList()[i].AttacksLeftThisTurn <= 0)
            {
                i = Random.Range(0,whoseTurn.AllyList().Count);
            }
            CreatureLogic cl = whoseTurn.AllyList()[i];
            if (cl.AttacksLeftThisTurn > 0)
            {
                cl.OnTurnEnd();

            }
        }
        yield return null;
    }


    bool CanACreatureAttack()
    {
        bool canAttack = false;
        for (int i = 0; i < whoseTurn.AllyList().Count; i++)
        {
            if (!whoseTurn.AllyList()[i].isDead && whoseTurn.AllyList()[i].AttacksLeftThisTurn > 0)
                canAttack = true;
        }
        return canAttack;
    }
    

    IEnumerator TurnEndCoroutine()
    {
        yield return StartCoroutine(EndTurnCoroutine());

        if(!whoseTurn.otherPlayer.gameIsOver)
            yield return StartCoroutine(StartTurnCoroutine());

        else
        {
            if(e_GameOver != null)
            e_GameOver.Invoke();
        }
    }

    IEnumerator EndTurnCoroutine()
    {              
        // stop timer
        timer.StopTimer();
        EndTurnCleanup();
        whoseTurn.OnTurnEnd();      
        yield return new WaitForSeconds(0.5f);

    }

    void EndTurnCleanup()
    {
        foreach (Player player in Player.Players)
        {                    
            
            foreach(CreatureLogic cl in player.AllyList())
            {
                if(cl.Health<=0 && !cl.isDead)     
                cl.Die();
            }
        }
    }

    IEnumerator StartTurnCoroutine()
    {
        endTurnForced = false;

        RoundReset();

        //yield return null;     


        if(TurnCounter<=0)
            {

              if(!isRoundOver)                
                new StartATurnCommand(whoseTurn.otherPlayer).AddToQueue();

              else
                new StartATurnCommand(firstPlayer).AddToQueue();
              
            }
        else            
        {              
            new StartATurnCommand(whoseTurn).AddToQueue();
        } 

        TurnCounter--;

        yield return new WaitForSeconds(3f);
        GlobalSettings.Instance.EndTurnButton.interactable = true;

        
    }


    public void StopTheTimer()
    {
        timer.StopTimer();
    }

    void RoundReset()
    {
        isRoundOver = true;


        foreach (Player p in Player.Players)
        {
            foreach(CreatureLogic cl in p.AllyList())
            {
                if (cl.isActive && cl.AttacksLeftThisTurn > 0)
                {
                    isRoundOver = false;

                }
            }
        }

        if (isRoundOver)
        {
            
            
            //End of Round
            if(e_EndOfRound != null)
                e_EndOfRound.Invoke();
            
            foreach (Player p in Player.Players)
            {
                p.isRoundOver = true;
                foreach (CreatureLogic cl in p.AllyList())
                {
                    if (!cl.isDead && !cl.hasStun)
                    {
                        //cl.isActive = true;
                        cl.OnTurnStart();

                        //DS reset "color" of creature
                        new CreatureColorCommand(cl,false).AddToQueue();

                   }
                }
            }

            roundCounter++;

            string roundString = "Round " + roundCounter + "!";

            new ShowMessageCommand(roundString, GlobalSettings.Instance.MessageTime).AddToQueue();

            if(e_ResetRound != null)
                e_ResetRound.Invoke();


        }

    }


}

