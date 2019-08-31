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
    private RopeTimer timer;

    public bool endTurnForced = false;


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
            p.PArea.Portrait.transform.position = p.PArea.InitialPortraitPosition.position;

        }

        Sequence s = DOTween.Sequence();
        s.Append(Player.Players[0].PArea.Portrait.transform.DOMove(Player.Players[0].PArea.PortraitPosition.position, 0.5f).SetEase(Ease.InQuad));
        s.Insert(0f, Player.Players[1].PArea.Portrait.transform.DOMove(Player.Players[1].PArea.PortraitPosition.position, 0.5f).SetEase(Ease.InQuad));
        s.PrependInterval(0.5f);  

        s.OnComplete(() =>
            {

         // determine who starts the game.
                int rnd = Random.Range(0,2);  // 2 is exclusive boundary
                // Debug.Log(Player.Players.Length);
                Player whoGoesFirst = Player.Players[rnd];
                // Debug.Log(whoGoesFirst);
                Player whoGoesSecond = whoGoesFirst.otherPlayer;
                // Debug.Log(whoGoesSecond);
         
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

       


                //DS
                //Display skills panel of player only
                if(whoGoesFirst.PArea.owner == AreaPosition.Low)
                new ShowSkillsPanelCommand(whoGoesFirst).AddToQueue();
                else
                new ShowSkillsPanelCommand(whoGoesSecond).AddToQueue();

                //DS
                //Initially, all creatures isActive
                //RoundReset();

                new StartATurnCommand(whoGoesFirst).AddToQueue();
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
         
        StartCoroutine(TurnEndCoroutine());        
    }

    IEnumerator TurnEndCoroutine()
    {
        yield return StartCoroutine(EndTurnCoroutine());
        //yield return new WaitForSeconds(0.5f);
        if(!whoseTurn.otherPlayer.gameIsOver)
            yield return StartCoroutine(StartTurnCoroutine());
    }


    IEnumerator EndTurnCoroutine()
    {        
        
            // stop timer
            timer.StopTimer();
            // send all commands in the end of current player`s turn
            whoseTurn.OnTurnEnd();

            //New SCRIPT - also end each creature's turn
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


         if(TurnCounter<=0)
            {
              //yield return new WaitForSeconds(0.5f);
              new StartATurnCommand(whoseTurn.otherPlayer).AddToQueue();
            }
            else            
            {
              //yield return new WaitForSeconds(0.5f);
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

}

