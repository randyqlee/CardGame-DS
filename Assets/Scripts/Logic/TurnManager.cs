using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;


// this class will take care of switching turns and counting down time until the turn expires
public class TurnManager : MonoBehaviour {

    // PUBLIC FIELDS
    public CardAsset CoinCard;

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



            //DS - use this if we want Round-based turns
            //Check if Round is not yet over, call Player OnTurnStart
            //Else, reset first then let player start
            //if (!RoundIsOver())
            //    tm.OnTurnStart();
            

            //else
            //{
            //    RoundReset();
            //    tm.OnTurnStart();
            //}

                        // player`s method OnTurnStart() will be called in tm.OnTurnStart();
            tm.OnTurnStart();

            if (tm is PlayerTurnMaker)
            {
            //DS    whoseTurn.HighlightPlayableCards();
            }
            // remove highlights for opponent.
            //DS whoseTurn.otherPlayer.HighlightPlayableCards(true);
                
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
        OnGameStart();
    }

    public void OnGameStart()
    {

        CardLogic.CardsCreatedThisGame.Clear();
        CreatureLogic.CreaturesCreatedThisGame.Clear();

        //DS - use this sequence later on for any Start of Battle loading/animation effects

        foreach (Player p in Player.Players)
        {
            p.ManaThisTurn = 0;
            p.ManaLeft = 0;
            p.LoadCharacterInfoFromAsset();
            p.TransmitInfoAboutPlayerToVisual();
            p.PArea.PDeck.CardsInDeck = p.deck.cards.Count;
            // move both portraits to the center
            //p.PArea.Portrait.transform.position = p.PArea.InitialPortraitPosition.position;

        }

        Sequence s = DOTween.Sequence();
        //s.Append(Player.Players[0].PArea.Portrait.transform.DOMove(Player.Players[0].PArea.PortraitPosition.position, 0.5f).SetEase(Ease.InQuad));
        //s.Insert(0f, Player.Players[1].PArea.Portrait.transform.DOMove(Player.Players[1].PArea.PortraitPosition.position, 0.5f).SetEase(Ease.InQuad));
        s.PrependInterval(0.5f);  

        s.OnComplete(() =>
            {

         // determine who starts the game.
         

                Player whoGoesFirst;
                Player whoGoesSecond;

                if (Tutorial1.Instance != null && Tutorial1.Instance.tutorialState != TutorialState.COMPLETED)
                {
                    whoGoesFirst = Player.Players[1];
                    // Debug.Log(whoGoesFirst);
                    whoGoesSecond = whoGoesFirst.otherPlayer;

                }

                else
                {
                    int rnd = Random.Range(0,2);  // 2 is exclusive boundary
                    // Debug.Log(Player.Players.Length);
                    whoGoesFirst = Player.Players[rnd];
                    // Debug.Log(whoGoesFirst);
                    whoGoesSecond = whoGoesFirst.otherPlayer;
                    // Debug.Log(whoGoesSecond);
                }
         
                // draw 4 cards for first player and 5 for second player
                int initDraw = GlobalSettings.Instance.HeroesCount;
                for (int i = 0; i < initDraw; i++)
                {            
                    // second player draws a card
                    whoGoesSecond.DrawACard(true);
                    // first player draws a card
                    whoGoesFirst.DrawACard(true);
                
                    //DS
                    //put the creatures on table
                    whoGoesSecond.PlayACreatureFromHand(whoGoesSecond.hand.CardsInHand[0], 0);
                    whoGoesFirst.PlayACreatureFromHand(whoGoesFirst.hand.CardsInHand[0], 0);

                }
/*
                int initDrawEquip = GlobalSettings.Instance.HeroesEquipCount;
                for (int i = 0; i < initDrawEquip; i++)
                {            
                    // second player draws a card
                    whoGoesSecond.DrawACard(true);
                    // first player draws a card
                    whoGoesFirst.DrawACard(true);
                
                    //DS
                    //play spell to use the equip ability of creature
                    
                    whoGoesSecond.PlayEquipCreatureFromHand(whoGoesSecond.hand.CardsInHand[0]);

                    
                    whoGoesFirst.PlayEquipCreatureFromHand(whoGoesFirst.hand.CardsInHand[0]);

                }
*/
                //SAVE Info on who's the original first player
                firstPlayer = whoGoesFirst;


                //DS
                //Display skills panel of player only

                if(Tutorial1.Instance == null || Tutorial1.Instance.tutorialState == TutorialState.COMPLETED)
                {
                    if(whoGoesFirst.PArea.owner == AreaPosition.Low)
                    new ShowSkillsPanelCommand(whoGoesFirst).AddToQueue();
                    else
                    new ShowSkillsPanelCommand(whoGoesSecond).AddToQueue();


                    new StartATurnCommand(whoGoesFirst).AddToQueue();
                }
                //DS
                //Initially, all creatures isActive
                //RoundReset();

                



                
            });


    }


//DS - Use this for Round-based turn
/*
    //DS
    // Reset creature turns for each round
    void RoundReset()
    {
        foreach (Player p in Player.Players)
        {               
            foreach (CreatureLogic cl in p.table.CreaturesOnTable)
            {
                //cl.isActive = true;
                if (!cl.isDead)
                    cl.OnTurnStart();
            }
        }
    }

    //DS
    //Check if Round should be over
    bool RoundIsOver()
    {
        foreach (Player p in Player.Players)
        {               
            foreach (CreatureLogic cl in p.table.CreaturesOnTable)
            {
                if (cl.isActive)
                    return false;
            }
        }
        return true;
    }

*/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EndTurn();
    }

    // FOR TEST PURPOSES ONLY
    public void EndTurnTest()
    {
        timer.StopTimer();
        timer.StartTimer();
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
            int i = Random.Range(0,whoseTurn.table.CreaturesOnTable.Count);
            while (whoseTurn.table.CreaturesOnTable[i].isDead || whoseTurn.table.CreaturesOnTable[i].AttacksLeftThisTurn <= 0)
            {
                i = Random.Range(0,whoseTurn.table.CreaturesOnTable.Count);
            }
            CreatureLogic cl = whoseTurn.table.CreaturesOnTable[i];
            if (cl.AttacksLeftThisTurn > 0)
            {
                cl.OnTurnEnd();

            }
        }
        
        else
        {
            EndTurn(); 
        }

        yield return null;
    }


    bool CanACreatureAttack()
    {
        bool canAttack = false;
        for (int i = 0; i < whoseTurn.table.CreaturesOnTable.Count; i++)
        {
            if (!whoseTurn.table.CreaturesOnTable[i].isDead && whoseTurn.table.CreaturesOnTable[i].AttacksLeftThisTurn > 0)
                canAttack = true;
        }
        return canAttack;
    }
    

    IEnumerator TurnEndCoroutine()
    {
        yield return StartCoroutine(EndTurnCoroutine());
        //yield return new WaitForSeconds(0.5f);
        if(!whoseTurn.otherPlayer.gameIsOver)
            yield return StartCoroutine(StartTurnCoroutine());

        else
        {
            e_GameOver.Invoke();
        }
    }


    IEnumerator EndTurnCoroutine()
    {        
        
            // stop timer
            timer.StopTimer();
            // send all commands in the end of current player`s turn
            whoseTurn.OnTurnEnd();

            //New SCRIPT - also end each creature's turn
            /*
            if (endTurnForced)
            {
                foreach (CreatureLogic cl in whoseTurn.table.CreaturesOnTable)
                {
                    if(cl.isActive)
                    {
                        cl.OnTurnEnd();
                    }
                }
            }     
            */   
             

        //     if(TurnCounter<=0)
        //     {
        //       new StartATurnCommand(whoseTurn.otherPlayer).AddToQueue();
        //     }
        //     else            
        //     {
        //       new StartATurnCommand(whoseTurn).AddToQueue();
        //     } 

        // TurnCounter--;

        yield return new WaitForSeconds(0.5f);
        
        //Command.CommandExecutionComplete();

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

        //DS
        //GlobalSettings.Instance.EndTurnButton.enabled = true;
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
            foreach(CreatureLogic cl in p.table.CreaturesOnTable)
            {
                if (cl.isActive)
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
                foreach (CreatureLogic cl in p.table.CreaturesOnTable)
                {
                    if (!cl.isDead)
                    {
                        cl.isActive = true;

                        //DS reset "color" of creature
                        new CreatureColorCommand(cl,false).AddToQueue();

                        
                        //Reduce ability cooldown
                        //foreach (CreatureEffect ce in cl.creatureEffects)
                        //{
                        //    ce.ReduceCreatureEffectCooldown();
                        //}

                        //for (int i = cl.buffEffects.Count-1; i >= 0; i--)
                        //{
                        //    cl.buffEffects[i].ReduceCreatureEffectCooldown();
                        //}
                    }
                }
            }

            /*
            foreach (ResetRound rr in e_ResetRound.GetInvocationList ())
            {
                try
                {
                    rr.Invoke();
                    Debug.Log ("Invoke");
                } catch(System.Exception e) {
                    Debug.Log ("Reset error: " + e.Message);
                }
            }
            */

            roundCounter++;

            string roundString = "Round " + roundCounter + "!";

            new ShowMessageCommand(roundString, GlobalSettings.Instance.MessageTime).AddToQueue();

            if(e_ResetRound != null)
                e_ResetRound.Invoke();


        }

    }


}

