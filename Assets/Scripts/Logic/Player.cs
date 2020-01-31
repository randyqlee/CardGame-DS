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

    //REFERENCCES To enemy and ally creatures (including self)
    public List<CreatureLogic> enemies = new List<CreatureLogic>();
    public List<CreatureLogic> allies = new List<CreatureLogic>();

    public List<CreatureLogic> deadAllies = new List<CreatureLogic>();

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

    public int health;

    public int Health
    {
        get{ return health; }
        set{}
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

    // CODE FOR EVENTS TO LET CREATURES KNOW WHEN TO CAUSE EFFECTS
    public delegate void VoidWithNoArguments();
    public event VoidWithNoArguments CreaturePlayedEvent;
    public event VoidWithNoArguments SpellPlayedEvent;
    public event VoidWithNoArguments StartTurnEvent;
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
        if (!cl.isDead)   
        {   

             cl.OnTurnStart();

            
        }
        isRoundOver = false;
    }


    public virtual void OnTurnStart()
    {

        if (isRoundOver)
        {
            OnReset();

        }

        bool hasActiveCL = false;

        foreach (CreatureLogic cl in table.CreaturesOnTable)
        {
            if (cl.isActive && cl.AttacksLeftThisTurn > 0)
                hasActiveCL = true;
        }

        if(!hasActiveCL)
            new EndTurnCommand().AddToQueue();

        else
        {
            //DS
            HighlightPlayableCards();
        }
    }

    

    public void OnTurnEnd()
    {
        
        GetComponent<TurnMaker>().StopAllCoroutines();
        HighlightPlayableCards(true);
        
    }

    public void CheckIfGameOver()
    {

        if (AllyList().Count <= 0)
        {
            Die();
        }

        if (EnemyList().Count <= 0)
        {
            otherPlayer.Die();
        }

    }

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

    // get card NOT from deck (a token or a coin)

//DS COMMENT OUT GETACARDNOTFROMDECK

/*
    public void GetACardNotFromDeck(CardAsset cardAsset)
    {
        if (hand.CardsInHand.Count < PArea.handVisual.slots.Children.Length)
        {
            // 1) logic: add card to hand
            CardLogic newCard = new CardLogic(cardAsset);
            newCard.owner = this;
            hand.CardsInHand.Insert(0, newCard);

            // 2) send message to the visual Deck
            new DrawACardCommand(hand.CardsInHand[0], this, fast: true, fromDeck: false).AddToQueue(); 
        }
        // no removal from deck because the card was not in the deck
    }

    //DS
    //overload of GetACardNotFromDeck
    public void GetACardNotFromDeck(CardAsset cardAsset, int heroID)
    {
        if (hand.CardsInHand.Count < PArea.handVisual.slots.Children.Length)
        {
            // 1) logic: add card to hand
            CardLogic newCard = new CardLogic(cardAsset);
            newCard.owner = this;
            newCard.heroID = heroID;
            hand.CardsInHand.Insert(0, newCard);

            // 2) send message to the visual Deck
            new DrawACardCommand(hand.CardsInHand[0], this, fast: true, fromDeck: false).AddToQueue(); 
        }
        // no removal from deck because the card was not in the deck
    }
*/


    // 2 METHODS FOR PLAYING SPELLS
    // 1st overload - takes ids as arguments
    // it is cnvenient to call this method from visual part
    public void PlayASpellFromHand(int SpellCardUniqueID, int TargetUniqueID)
    {
        if (TargetUniqueID < 0)
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], null);
        else if (TargetUniqueID == ID)
        {
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], this);
        }
        else if (TargetUniqueID == otherPlayer.ID)
        {
            PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], this.otherPlayer);
        }
        else
        {
            // target is a creature
            //PlayASpellFromHand(CardLogic.CardsCreatedThisGame[SpellCardUniqueID], CreatureLogic.CreaturesCreatedThisGame[TargetUniqueID]);
        }          
    }

    // 2nd overload - takes CardLogic and ICharacter interface - 
    // this method is called from Logic, for example by AI
    public void PlayASpellFromHand(CardLogic playedCard, ICharacter target)
    {
 //       ManaLeft -= playedCard.CurrentManaCost;
        // cause effect instantly:
        if (playedCard.effect != null)
            playedCard.effect.ActivateEffect(playedCard.ca.specialSpellAmount, target);
        else
        {
            Debug.LogWarning("No effect found on card " + playedCard.ca.name);
        }
        // no matter what happens, move this card to PlayACardSpot
        new PlayASpellCardCommand(this, playedCard).AddToQueue();
        // remove this card from hand
        hand.CardsInHand.Remove(playedCard);
        // check if this is a creature or a spell
    }


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
            if (!cl.isDead)
            {
                
                GameObject g = IDHolder.GetGameObjectWithID(cl.UniqueCreatureID);
                if(g!= null)
                //DS
                {
                    g.GetComponent<OneCreatureManager>().CanAttackNow = (cl.AttacksLeftThisTurn > 0) && !removeAllHighlights;                                  
                }
            }     
        }   
    }

    //DS

    public void HideHand()
    {
        foreach(OneCardManager card in PArea.handVisual.slots.GetComponentsInChildren<OneCardManager>())
        {
            card.gameObject.GetComponentInChildren<Canvas>().gameObject.GetComponent<Canvas>().enabled = false;
        }

    }

    public void ShowHand(CreatureLogic crl)
    {
        foreach(CardLogic cl in hand.CardsInHand)
        {
            if(cl.heroID == crl.UniqueCreatureID)
            {
                foreach(OneCardManager card in PArea.handVisual.slots.GetComponentsInChildren<OneCardManager>())
                {
                    if(card.gameObject.GetComponent<IDHolder>().UniqueID == cl.UniqueCardID)
                        card.gameObject.GetComponentInChildren<Canvas>().gameObject.GetComponent<Canvas>().enabled = true;
                }

            }
        }
    }

    public void ClearHand()
    {
        foreach(CardLogic cl in hand.CardsInHand)
        {
            new PlayASpellCardCommand(this, cl).AddToQueue();
            hand.CardsInHand.Remove(cl);
        }
    }
    //DS

    //DS
/*
    public void DrawAbilityCards(CreatureLogic crl)
    {
    
        foreach (CardAsset ca in crl.abilities)
        {
            
           GetACardNotFromDeck(ca,crl.UniqueCreatureID);
        }
    }
    //DS
*/
    // START GAME METHODS


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
        enemies.Clear();

        foreach(KeyValuePair<int, CreatureLogic> creature in CreatureLogic.CreaturesCreatedThisGame)
       {
           CreatureLogic value = creature.Value;
           if(value.owner != this && !value.isDead && !value.isEquip)
           {
               enemies.Add(value);
           }                     
       }

       return enemies;
    }

    public List<CreatureLogic> AllyList()
    {
        allies.Clear();

        foreach(KeyValuePair<int, CreatureLogic> creature in CreatureLogic.CreaturesCreatedThisGame)
       {
           CreatureLogic value = creature.Value;
           if(value.owner == this && !value.isDead && !value.isEquip)
           {
               allies.Add(value);
           }                     
       }

       return allies;
    }

    public CreatureLogic GetRandomAlly (CreatureLogic exclude = null)
    {
        if (exclude == null)
        {
            int i = Random.Range(0,AllyList().Count);
            return AllyList()[i];
        }
        else
        {
            int i = Random.Range(0,AllyList().Count);
            while (AllyList()[i] == exclude)
            {
                i = Random.Range(0,AllyList().Count);
            }
            return AllyList()[i];
        }
    }

    public List<CreatureLogic> DeadAllyList()
    {
        deadAllies.Clear();

        foreach(KeyValuePair<int, CreatureLogic> creature in CreatureLogic.CreaturesCreatedThisGame)
       {
           CreatureLogic value = creature.Value;
           if(value.owner == this && value.isDead && !value.isEquip)
           {
               deadAllies.Add(value);
           }                     
       }

       return deadAllies;
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
