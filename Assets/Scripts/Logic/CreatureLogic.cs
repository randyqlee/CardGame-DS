using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CreatureLogic: ICharacter
{
    // PUBLIC FIELDS
    public Player owner;
    public CardAsset ca;
    public CreatureEffect effect;
    public int UniqueCreatureID;
    public bool Frozen = false;
    public bool isDead;
    public bool isActive;

    //DS
    //Adding Abilities
    public List<CardAsset> abilities;    
    
    public List<CreatureEffect> creatureEffects = new List<CreatureEffect>(); 

    public List<BuffEffect> buffEffects = new List<BuffEffect>(); 

    //public List<CreatureEffect> creatureEffects; 

    //public List<BuffEffect> buffEffects;

   public delegate void CreatureOnTurnStart();    
    public event CreatureOnTurnStart e_CreatureOnTurnStart;

    public delegate void CreatureOnTurnEnd();    
    public event CreatureOnTurnEnd e_CreatureOnTurnEnd;   

    public delegate void CreatureIsAttacked();    
    public event CreatureIsAttacked e_CreatureIsAttacked;   
    
    //Debuff flags
    public bool isAttacked;   
    
    
    // PROPERTIES
    // property from ICharacter interface
    public int ID
    {
        get{ return UniqueCreatureID; }
    }
        
    // the basic health that we have in CardAsset
    private int baseHealth;
    // health with all the current buffs taken into account
    public int MaxHealth
    {
        get{ return baseHealth;}
    }

    // current health of this creature
    private int health;
    public int Health
    {
        get{ return health; }

        set
        {
            if (value > MaxHealth)
                health = MaxHealth;
            else if (value <= 0)
                Die();
            else
                health = value;
        }
    }

    // returns true if we can attack with this creature now
    public bool CanAttack
    {

        get
        {
            bool ownersTurn = (TurnManager.Instance.whoseTurn == owner);
            return (ownersTurn && (AttacksLeftThisTurn > 0) && !Frozen);
        }
        
        
    }

    // property for Attack
    private int baseAttack;
    public int Attack
    {
        get{ return baseAttack; }

        set{ baseAttack = value;}
    }
     
    // number of attacks for one turn if (attacksForOneTurn==2) => Windfury
    private int attacksForOneTurn = 1;

    private int attacksLeftThisTurn;
    public int AttacksLeftThisTurn
    {
        get{
            return attacksLeftThisTurn;
        }

        set{
            if(value < 0)
                attacksLeftThisTurn = 0;
            else
                attacksLeftThisTurn = value;
        }
    }

    // CONSTRUCTOR
    public CreatureLogic(Player owner, CardAsset ca)
    {
        this.ca = ca;

        //DS
        abilities = ca.Abilities;


        baseHealth = ca.MaxHealth;
        Health = ca.MaxHealth;
        baseAttack = ca.Attack;
        
        attacksForOneTurn = ca.AttacksForOneTurn;

        // Remove Charge
        // if (ca.Charge)
        //     AttacksLeftThisTurn = attacksForOneTurn;

        this.owner = owner;
        UniqueCreatureID = IDFactory.GetUniqueID(); 

        //DS

        //creatureEffects = new List<CreatureEffect>(); 

        //buffEffects = new List<BuffEffect>();        
       
        //DS
        //Add activator for abilities
        if (ca.abilityEffect != null)
        {
            foreach (AbilityEffect ae in ca.abilityEffect)
            {
                if (ae.CreatureScriptName != null && ae.CreatureScriptName != "")
                {
                    effect = System.Activator.CreateInstance(System.Type.GetType(ae.CreatureScriptName), new System.Object[]{owner, this, ae.coolDown}) as CreatureEffect;
                    effect.RegisterCooldown();
                    effect.RegisterEventEffect();
                    creatureEffects.Add(effect);
                }
            }
        }

        //DS

        CreaturesCreatedThisGame.Add(UniqueCreatureID, this);
    }

    
    // METHODS
    public void OnTurnStart()
    {
        // will be granted by Player
        isActive = true;
        AttacksLeftThisTurn = attacksForOneTurn; 

        //TurnOrder:  Check Stun, Ability Cooldown Reduction, Effects

        //TODO:  Check Stun, Skip Turn.  Don't load Effects and EndTurn.

        //TODO: Buff/Debuff effects (like Poison, Heal)
       
        if(e_CreatureOnTurnStart != null)
            e_CreatureOnTurnStart.Invoke();

         //TODO:  Ability Effects (BattleCry, etc.)
        foreach(CreatureEffect ce in creatureEffects)
        {
            //Debug.Log ("CreatureEffect: " + ce.ToString() + ", CD: " + ce.remainingCooldown);
        }
                
    }

    public void OnTurnEnd(){
        isActive = false;
        AttacksLeftThisTurn = 0;

        //TODO:  End of Turn Effects
        if(e_CreatureOnTurnEnd != null)
            e_CreatureOnTurnEnd.Invoke();
        
        //TODO:  Buff Duration Reduction          
              
    }

    public void Die()
    {   
        //ORIGINAL SCRIPT
        //owner.table.CreaturesOnTable.Remove(this);        
        
        //New SCRIPT
        this.isDead = true;

        
        //Remove all buffs/debuffs        
        RemoveAllBuffs(); 

        // cause Deathrattle Effect
        if (effect != null)
        {
            effect.WhenACreatureDies();
            effect.UnRegisterEventEffect();
            effect.UnregisterCooldown();            
            effect = null;
            
            creatureEffects.Clear();
        }

        new CreatureDieCommand(UniqueCreatureID, owner).AddToQueue();  

        owner.CheckIfGameOver();   

        
    }

    public void GoFace()
    {
        AttacksLeftThisTurn--;
        int targetHealthAfter = owner.otherPlayer.Health - Attack;
        new CreatureAttackCommand(owner.otherPlayer.PlayerID, UniqueCreatureID, 0, Attack, Health, targetHealthAfter).AddToQueue();
        owner.otherPlayer.Health -= Attack;
    }

    public void AttackCreature (CreatureLogic target)
    {
        
        AttacksLeftThisTurn--;         
        
        // calculate the values so that the creature does not fire the DIE command before the Attack command is sent
        int targetHealthAfter = target.Health - Damage(Attack);

        //original
        //int attackerHealthAfter = Health - target.Attack;

        int attackerHealthAfter = Health;

        //original
        //new CreatureAttackCommand(target.UniqueCreatureID, UniqueCreatureID, target.Attack, Attack, attackerHealthAfter, targetHealthAfter).AddToQueue();

        //set target attack to 0 to reflect non-damage for the attacker
        new CreatureAttackCommand(target.UniqueCreatureID, UniqueCreatureID, 0, Attack, attackerHealthAfter, targetHealthAfter).AddToQueue();

        target.Health -= Damage(Attack);
        
        //originally enabled
        //Health -= target.Attack;       

        if(target.e_CreatureIsAttacked != null)
            target.e_CreatureIsAttacked.Invoke();        
        
        //DS
        foreach(CreatureEffect ce in creatureEffects)
        {
            //Debug.Log ("CreatureEffect: " + ce.ToString() + ", CD: " + ca.specialSpellAmount);
            if(ce.creatureEffectCooldown == 0)
            {
                Debug.Log ("Using CreatureEffect: " + ce.ToString());
                ce.UseEffect(target);

            }
        }

        
    }//Attack Creature

    //Method to override for damage modifiers
    public virtual int Damage(int damage)
    {
        return damage;
    }



    public void AttackCreatureWithID(int uniqueCreatureID)
    {
        CreatureLogic target = CreatureLogic.CreaturesCreatedThisGame[uniqueCreatureID];
        AttackCreature(target);
    }

    public void AddBuff(BuffEffect buff)
    {
        bool buffExists = false;

        //check if same buff already exists, just update the buff
        foreach (BuffEffect be in buffEffects)
        {
            if (be.GetType().Name == buff.GetType().Name)
            {
                be.source = buff.source;
                be.target = buff.target;
                
                if (be.buffCooldown < buff.buffCooldown)
                    be.buffCooldown = buff.buffCooldown;

                buffExists = true;
            }

        }

        //if buff is new, add it to the CL

        if (!buffExists)
        {
            buff.RegisterCooldown();
            
            buff.CauseBuffEffect();
            buffEffects.Add(buff);
        }
        

    }   

    public void RemoveBuff(BuffEffect buff)
    {

    }

    public void RemoveAllBuffs()
    {
        foreach (BuffEffect be in buffEffects)
        {          
            be.UndoBuffEffect();                
            be.UnregisterCooldown();                         
        }
        buffEffects.Clear();
    }//RemoveAllBuffs

    
    // STATIC For managing IDs
    public static Dictionary<int, CreatureLogic> CreaturesCreatedThisGame = new Dictionary<int, CreatureLogic>();

}
