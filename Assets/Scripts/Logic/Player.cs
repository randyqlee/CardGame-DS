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
    public SpellEffect HeroPowerEffect;
    // a flag not to use hero power twice
    public bool usedHeroPowerThisTurn = false;


    //REFERENCCES To enemy and ally creatures (including self)
    public List<CreatureLogic> enemies = new List<CreatureLogic>();
    public List<CreatureLogic> allies = new List<CreatureLogic>();

    // REFERENCES TO LOGICAL STUFF THAT BELONGS TO THIS PLAYER
    public Deck deck;
    public Hand hand;
    public Table table;

    // a static array that will store both players, should always have 2 players
    public static Player[] Players;

    // this value used exclusively for our coin spell
    private int bonusManaThisTurn = 0;
    private int creatureTurn;
    private int extraCreatureTurn=0;

    public int ExtraCreatureTurn
    {
        get{return extraCreatureTurn;}
        set{
            if(value >= 1)
            extraCreatureTurn = 1;
            else
            extraCreatureTurn = 0;
        }
    }


    private List<bool> isDeadStatus = new List<bool>();
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

    // total mana crystals that this player has this turn
    private int manaThisTurn;
    public int ManaThisTurn
    {
        get{ return manaThisTurn;}
        set
        {
            if (value < 0)
                manaThisTurn = 0;
            else if (value > PArea.ManaBar.Crystals.Length)
                manaThisTurn = PArea.ManaBar.Crystals.Length;
            else
                manaThisTurn = value;
            //PArea.ManaBar.TotalCrystals = manaThisTurn;
            // DS new UpdateManaCrystalsCommand(this, manaThisTurn, manaLeft).AddToQueue();
        }
    }

    // full mana crystals available right now to play cards / use hero power 
    private int manaLeft;
    public int ManaLeft
    {
        get
        { return manaLeft;}
        set
        {
            if (value < 0)
                manaLeft = 0;
            else if (value > PArea.ManaBar.Crystals.Length)
                manaLeft = PArea.ManaBar.Crystals.Length;
            else
                manaLeft = value;
            
            //PArea.ManaBar.AvailableCrystals = manaLeft;
            //DS new UpdateManaCrystalsCommand(this, ManaThisTurn, manaLeft).AddToQueue();
            //Debug.Log(ManaLeft);

            //DS
            //comment out
            //if (TurnManager.Instance.whoseTurn == this)
            //    HighlightPlayableCards();
        }
    }

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
            
            //DS
            // if (value <= 0)
            //     Die(); 
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
                //DS random pick if AI
        if (GetComponent<TurnMaker>() is AITurnMaker)
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
            if (cl.isActive)
                hasActiveCL = true;
        }

        if(!hasActiveCL)
            new EndTurnCommand().AddToQueue();


        // add one mana crystal to the pool;
        //Debug.Log("In ONTURNSTART for "+ gameObject.name);
        usedHeroPowerThisTurn = false;
        ManaThisTurn++;
        ManaLeft = ManaThisTurn;
        
        //ORIGINAL SCRIPT
        //foreach (CreatureLogic cl in table.CreaturesOnTable)
        //  cl.OnTurnStart();
        
        
        
        //DS: Use this if you want each PLayer Turn all creatures are active
        //foreach (CreatureLogic cl in table.CreaturesOnTable)  
        //    if (!cl.isDead)   
        //    {   
        //     cl.OnTurnStart();
        //     cl.e_CreatureOnTurnEnd += OnTurnEnd;
        //    }
        // PArea.HeroPower.WasUsedThisTurn = false;

        //logic to check if creature not dead


        //DS
        //HideHand();
        //otherPlayer.HideHand();

        //DS
        //Iterate through Creatures on table so that only 1 creature gets active each turn
       // Debug.Log("Creature Index: " +creatureTurn);       
    
      
      //DS

/*
       if(!gameIsOver)
       {
           //if dead, pass on the turn to other player
           if (table.CreaturesOnTable[creatureTurn].isDead)
           { 
                table.CreaturesOnTable[creatureTurn].OnTurnEnd();
                TurnManager.Instance.EndTurn();

           }




           //iterate if dead
           //while(table.CreaturesOnTable[creatureTurn].isDead)
           //{
           // if(creatureTurn < table.CreaturesOnTable.Count)
           //     creatureTurn++;                         
           // if(creatureTurn >= table.CreaturesOnTable.Count)
           //     creatureTurn = 0; 
          // }

          else
          {

            creatureTurn -= ExtraCreatureTurn;
            if(creatureTurn<0)
            creatureTurn = table.CreaturesOnTable.Count-1;

         //DS
         //Remove for Round reset testing
         //table.CreaturesOnTable[creatureTurn].OnTurnStart(); 

            table.CreaturesOnTable[creatureTurn].OnTurnStart(); 
            //ShowHand(table.CreaturesOnTable[creatureTurn]);
          }
            //DS
            //DrawAbilityCards(table.CreaturesOnTable[creatureTurn]);

            if(creatureTurn < table.CreaturesOnTable.Count)
                creatureTurn++;                              
            if(creatureTurn >= table.CreaturesOnTable.Count)
                creatureTurn = 0;

            ExtraCreatureTurn--;
       }     
      
      //table.CreaturesOnTable[creatureTurn].OnTurnStart();

*/  

               
        PArea.HeroPower.WasUsedThisTurn = false;

        //DS
        HighlightPlayableCards();
    }

    

    public void OnTurnEnd()
    {

//        if(EndTurnEvent != null)
//            EndTurnEvent.Invoke();
        ManaThisTurn -= bonusManaThisTurn;
        bonusManaThisTurn = 0;
        GetComponent<TurnMaker>().StopAllCoroutines();

        //new EndTurnCommand().AddToQueue();

        //Check if Game Over - all creatures empty
        //CheckIfGameOver();

        //cl active this turn needs to have attacksleft this turn set to 0

        //DS
        HighlightPlayableCards(true);


 /*       
        bool allCreaturesFinished = true;
        foreach (CreatureLogic cl in table.CreaturesOnTable)  
            if (cl.isActive)   
            {
                allCreaturesFinished = false;
                break;
            }

        if (allCreaturesFinished)
        {
            if(EndTurnEvent != null)
                EndTurnEvent.Invoke();
            ManaThisTurn -= bonusManaThisTurn;
            bonusManaThisTurn = 0;
            GetComponent<TurnMaker>().StopAllCoroutines();

            new EndTurnCommand().AddToQueue();

            //Check if Game Over - all creatures empty
            //CheckIfGameOver();

            //cl active this turn needs to have attacksleft this turn set to 0

            //DS
            HighlightPlayableCards(true);
        }
*/
        
    }

    public void CheckIfGameOver()
    {

      
      isDeadStatus.Clear();
      otherPlayer.isDeadStatus.Clear();

      foreach(CreatureLogic cl in table.CreaturesOnTable){
          isDeadStatus.Add(cl.isDead);     
      } 

      foreach(CreatureLogic cl in otherPlayer.table.CreaturesOnTable){
          otherPlayer.isDeadStatus.Add(cl.isDead);     
      } 

        if(!isDeadStatus.Contains(false))
            Die();
            //return true;
        else if(!otherPlayer.isDeadStatus.Contains(false))
            otherPlayer.Die();
            //return true;
        

    }//Check if Game is Over

    // STUFF THAT OUR PLAYER CAN DO

    // get mana from coin or other spells 
    public void GetBonusMana(int amount)
    {
        bonusManaThisTurn += amount;
        ManaThisTurn += amount;
        ManaLeft += amount;
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
        ManaLeft -= playedCard.CurrentManaCost;
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
        // Debug.Log(ManaLeft);
        // Debug.Log(playedCard.CurrentManaCost);
        ManaLeft -= playedCard.CurrentManaCost;
        // Debug.Log("Mana Left after played a creature: " + ManaLeft);
        // create a new creature object and add it to Table
        CreatureLogic newCreature = new CreatureLogic(this, playedCard.ca);

        //DS
        //DS this is just an optional script if we want to monitor all CL in game
        //no logical or visual function
        //GameObject.Find("CardLogic").GetComponent<AllCardLogic>().creatureLogic.Add(newCreature);

        //HeroLogic newCreature = new HeroLogic(this, playedCard.ca);
        table.CreaturesOnTable.Insert(tablePos, newCreature);
        // 
        new PlayACreatureCommand(playedCard, this, tablePos, newCreature.UniqueCreatureID).AddToQueue();
        // cause battlecry Effect
        if (newCreature.isEquip)
        {
        
            if (newCreature.effect != null)
                newCreature.effect.WhenACreatureIsPlayed();
        }
        // remove this card from hand
        hand.CardsInHand.Remove(playedCard);

        //DS
        //comment out
        //HighlightPlayableCards();

        //DS
        //DS COMMENT OUT GETACARDNOTFROMDECK
        //DrawAbilityCards(newCreature);
    }

    public void PlayEquipCreatureFromHand(CardLogic playedCard)
    {
        // Debug.Log(ManaLeft);
        // Debug.Log(playedCard.CurrentManaCost);
        ManaLeft -= playedCard.CurrentManaCost;
        // Debug.Log("Mana Left after played a creature: " + ManaLeft);
        // create a new creature object and add it to Table
        CreatureLogic newCreature = new CreatureLogic(this, playedCard.ca);

        newCreature.isEquip = true;

  
        //DS
        //DS this is just an optional script if we want to monitor all CL in game
        //no logical or visual function
        //GameObject.Find("CardLogic").GetComponent<AllCardLogic>().creatureLogic.Add(newCreature);

        //HeroLogic newCreature = new HeroLogic(this, playedCard.ca);
        //table.CreaturesOnTable.Insert(tablePos, newCreature);
        // 
        //new PlayACreatureCommand(playedCard, this, tablePos, newCreature.UniqueCreatureID).AddToQueue();
        // cause battlecry Effect
        if (newCreature.isEquip)
        {
        
            if (newCreature.equipEffect != null)
            
                newCreature.equipEffect.WhenACreatureIsPlayed();
        }
        // remove this card from hand
        hand.CardsInHand.Remove(playedCard);

        //DS
        //comment out
        //HighlightPlayableCards();

        //DS
        //DS COMMENT OUT GETACARDNOTFROMDECK
        //DrawAbilityCards(newCreature);
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

    // use hero power - activate is effect like you`ve payed a spell
    public void UseHeroPower()
    {
        ManaLeft -= 2;
        usedHeroPowerThisTurn = true;
        HeroPowerEffect.ActivateEffect();
    }

    // METHOD TO SHOW GLOW HIGHLIGHTS
    public void HighlightPlayableCards(bool removeAllHighlights = false)
    {
        //Debug.Log("HighlightPlayable remove: "+ removeAllHighlights);
        foreach (CardLogic cl in hand.CardsInHand)
        {
            GameObject g = IDHolder.GetGameObjectWithID(cl.UniqueCardID);
            if (g!=null)
                g.GetComponent<OneCardManager>().CanBePlayedNow = (cl.CurrentManaCost <= ManaLeft) && !removeAllHighlights;
        }

        foreach (CreatureLogic crl in table.CreaturesOnTable)
        {
            if (!crl.isDead)
            {
                
                GameObject g = IDHolder.GetGameObjectWithID(crl.UniqueCreatureID);
                if(g!= null)
                //DS
                {
                    g.GetComponent<OneCreatureManager>().CanAttackNow = (crl.AttacksLeftThisTurn > 0) && !removeAllHighlights;
                    
                    
                }

                //DS
                //insert here script to clear the hand and call the "Hand" or Abilities for the HIghlighted Creature
                
                //ClearHand();
                //DrawAbilityCards(crl);

                //DS
            }
            
           
        }   
        // highlight hero power
        PArea.HeroPower.Highlighted = (!usedHeroPowerThisTurn) && (ManaLeft > 1) && !removeAllHighlights;

        //DS
        //HideHand();
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
    public void LoadCharacterInfoFromAsset()
    {
        Health = charAsset.MaxHealth;
        // change the visuals for portrait, hero power, etc...
        PArea.Portrait.charAsset = charAsset;
        PArea.Portrait.ApplyLookFromAsset();

        if (charAsset.HeroPowerName != null && charAsset.HeroPowerName != "")
        {
            HeroPowerEffect = System.Activator.CreateInstance(System.Type.GetType(charAsset.HeroPowerName)) as SpellEffect;
        }
        else
        {
            Debug.LogWarning("Check hero powr name for character " + charAsset.ClassName);
        }
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
       
        
}
