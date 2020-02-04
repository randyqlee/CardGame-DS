using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player : MonoBehaviour, ICharacter
{
    //DS
    public bool isRoundOver;

    // PUBLIC FIELDS
    // int ID that we get from ID factory
    public int PlayerID;
    // a Character Asset that contains data about this Hero
    public CharacterAsset charAsset;
    // a script with references to all the visual game objects for this player
    public PlayerArea PArea;
    // a script of type Spell effect that will be used for our hero power
    // (essenitially, using hero power is like playing a spell in a way)


    // REFERENCES TO LOGICAL STUFF THAT BELONGS TO THIS PLAYER
    public Deck deck;
    public Hand hand;
    public Table table;

    // a static array that will store both players, should always have 2 players
    public static Player[] Players;
    public bool gameIsOver = false;


    // PROPERTIES 
    // this property is a part of interface ICharacter
    public int ID
    {
        get{ return PlayerID; }
    }

    // opponent player
    public Player otherPlayer
    {
        get
        {
            if (Players[0] == this)
                return Players[1];
            else
                return Players[0];
        }
    }

//ONLY NEEDED DUE TO INTERFACE
    private int health;

    public int Health
    {
        get { return health;}
        set
        {
            if (value > charAsset.MaxHealth)
                health = charAsset.MaxHealth;
            else
                health = value;
        }
    }

    // CODE FOR EVENTS TO LET CREATURES KNOW WHEN TO CAUSE EFFECTS
    public delegate void VoidWithNoArguments();
    //public event VoidWithNoArguments CreaturePlayedEvent;
    //public event VoidWithNoArguments SpellPlayedEvent;
    //public event VoidWithNoArguments StartTurnEvent;
    public event VoidWithNoArguments EndTurnEvent;

       


    // ALL METHODS
    void Awake()
    {
        // find all scripts of type Player and store them in Players array
        // (we should have only 2 players in the scene)
        Players = GameObject.FindObjectsOfType<Player>();
        // obtain unique id from IDFactory
        PlayerID = IDFactory.GetUniqueID();
        isRoundOver = true;
    }

    void Start()
    {
                //DS random pick if AI, and no
        if (GetComponent<TurnMaker>() is AITurnMaker && BattleStartInfo.EnemyDeck == null)
        {
            CardAsset[] allCardsArray = Resources.LoadAll<CardAsset>("");
            List<CardAsset> allCardsList = new List<CardAsset>(allCardsArray);
            for (int i = 0; i < GlobalSettings.Instance.HeroesCount + GlobalSettings.Instance.HeroesEquipCount; i ++)
            {
                int index = Random.Range(0, allCardsList.Count);
                deck.cards[i] = allCardsList[index];

                Debug.Log ("AI Random Pick " + i + ": " + allCardsList[index].name);
                allCardsList.RemoveAt(index);
            }
        }
    }

    public void OnReset()
    {
        foreach (CreatureLogic cl in table.CreaturesOnTable)  
        {   
             cl.OnTurnStart();            
        }
        isRoundOver = false;
    }


    public virtual void OnTurnStart()
    {
        PArea.ControlsON = true;

        //check if Round is over
        if (isRoundOver)
        {
            OnReset();
        }

        //flag to test if a CL is active for a player for this turn. if none, end player's turn
        bool hasActiveCL = false;
        foreach (CreatureLogic cl in table.CreaturesOnTable)
        {
            if (cl.isActive && cl.AttacksLeftThisTurn > 0)
            {
                hasActiveCL = true;
                break;
            }
        }
        //if no active CL for the player, end turn
        if(!hasActiveCL)
            new EndTurnCommand().AddToQueue();
        else //highlight playable creatures
        {
            HighlightPlayableCards();
        }
    }

    

    public void OnTurnEnd()
    {
        GetComponent<TurnMaker>().StopAllCoroutines();
        HighlightPlayableCards(true);
        PArea.ControlsON = false;
    }

    public void CheckIfGameOver()
    {

        if (table.CreaturesOnTable.Count == 0)
        {
            Die();
        }

    }//Check if Game is Over

    // FOR TESTING ONLY
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            DrawACard();
    }

    // draw a single card from the deck
    public void DrawACard(bool fast = false)
    {
        if (deck.cards.Count > 0)
        {
            if (hand.CardsInHand.Count < PArea.handVisual.slots.Children.Length)
            {
                // 1) logic: add card to hand
                CardLogic newCard = new CardLogic(deck.cards[0]);
                newCard.owner = this;
                hand.CardsInHand.Insert(0, newCard);
                // Debug.Log(hand.CardsInHand.Count);
                // 2) logic: remove the card from the deck
                deck.cards.RemoveAt(0);
                // 2) create a command
                new DrawACardCommand(hand.CardsInHand[0], this, fast, fromDeck: true).AddToQueue();                 
            }
        }
        else
        {
            // there are no cards in the deck, take fatigue damage.
        }
       
    }

    // 2 METHODS FOR PLAYING SPELLS
    // 1st overload - takes ids as arguments
    // it is cnvenient to call this method from visual part

    // METHODS TO PLAY CREATURES 
    // 1st overload - by ID
    public void PlayACreatureFromHand(int UniqueID, int tablePos)
    {
        PlayACreatureFromHand(CardLogic.CardsCreatedThisGame[UniqueID], tablePos);
    }

    // 2nd overload - by logic units
    public void PlayACreatureFromHand(CardLogic playedCard, int tablePos)
    {
        CreatureLogic newCreature = new CreatureLogic(this, playedCard.ca);
        table.CreaturesOnTable.Insert(tablePos, newCreature);
        new PlayACreatureCommand(playedCard, this, tablePos, newCreature.UniqueCreatureID).AddToQueue();

        if (newCreature.isEquip)
        {
            if (newCreature.effect != null)
                newCreature.effect.WhenACreatureIsPlayed();
        }
        hand.CardsInHand.Remove(playedCard);
    }

    public void PlayEquipCreatureFromHand(CardLogic playedCard)
    {
        CreatureLogic newCreature = new CreatureLogic(this, playedCard.ca);
        newCreature.isEquip = true;
        if (newCreature.isEquip)
        {
            if (newCreature.equipEffect != null)
            {
                newCreature.equipEffect.WhenACreatureIsPlayed();
            }
        }
        // remove this card from hand
        hand.CardsInHand.Remove(playedCard);
    }
    public void Die()
    {
        // game over
        // block both players from taking new moves 
        gameIsOver = true;
        PArea.ControlsON = false;
        otherPlayer.PArea.ControlsON = false;
        TurnManager.Instance.StopTheTimer();
        new GameOverCommand(this).AddToQueue();
    }


    // METHOD TO SHOW GLOW HIGHLIGHTS
    public void HighlightPlayableCards(bool removeAllHighlights = false)
    {
        foreach (CreatureLogic cl in table.CreaturesOnTable)
        {
            GameObject g = IDHolder.GetGameObjectWithID(cl.UniqueCreatureID);
            if(g!= null)
            //DS
            {
                g.GetComponent<OneCreatureManager>().CanAttackNow = (cl.AttacksLeftThisTurn > 0) && !removeAllHighlights && PArea.ControlsON;                                  
            }
        }   
    }

    // START GAME METHODS
    public void LoadCharacterInfoFromAsset()
    {
        Health = charAsset.MaxHealth;
        // change the visuals for portrait, hero power, etc...
        PArea.Portrait.charAsset = charAsset;
        PArea.Portrait.ApplyLookFromAsset();
    }

    public void TransmitInfoAboutPlayerToVisual()
    {
        PArea.Portrait.gameObject.AddComponent<IDHolder>().UniqueID = PlayerID;
        if (GetComponent<TurnMaker>() is AITurnMaker)
        {
            // turn off turn making for this character
            PArea.AllowedToControlThisPlayer = false;
        }
        else
        {
            // allow turn making for this character
            PArea.AllowedToControlThisPlayer = true;
        }
    }


    public List<CreatureLogic> EnemyList()
    {
        return otherPlayer.table.CreaturesOnTable;
    }

    public List<CreatureLogic> AllyList()
    {
        return table.CreaturesOnTable;
    }

    public CreatureLogic GetRandomAlly (CreatureLogic exclude = null)
    {
        return GetRandomCreatureFromList (AllyList(), exclude);
    }

    public List<CreatureLogic> DeadAllyList()
    {
        return table.CreaturesOnGraveyard;
    }

    public CreatureLogic GetRandomCreatureFromList (List<CreatureLogic> creatures, CreatureLogic exclude = null)
    {
        if (exclude == null)
        {
            int i = Random.Range(0,creatures.Count);
            return creatures[i];
        }
        else
        {
            int i = Random.Range(0,creatures.Count);
            while (creatures[i] == exclude)
            {
                i = Random.Range(0,creatures.Count);
            }
            return creatures[i];
        }
    }
    public List<CreatureLogic> SortAllyListByHealth()
    {
        var returnList = AllyList();
        returnList.Sort(CompareListByHealth);
        return returnList;
    }

    private static int CompareListByHealth(CreatureLogic i1, CreatureLogic i2)
    {
        return i1.Health.CompareTo(i2.Health); 
    }
       
       
        
}
