using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CreatureLogic: ICharacter
{
    // PUBLIC FIELDS
    public Player owner;
    public string Name;
    public CardAsset ca;
    public CreatureEffect effect;
    public int UniqueCreatureID;
    public bool Frozen = false;
    public bool isDead;
    public bool isActive;
    public int chance;

    public bool isEquip = false;

    //DS
    //Adding Abilities
    public List<AbilityAsset> abilities; 

    public AbilityAsset equipAbility;   
    
    public List<CreatureEffect> creatureEffects = new List<CreatureEffect>(); 

    public CreatureEffect equipEffect;

    public List<BuffEffect> buffEffects = new List<BuffEffect>(); 

    //public List<CreatureEffect> creatureEffects; 

    //public List<BuffEffect> buffEffects;

    public delegate void CreatureOnTurnStart();    
    public event CreatureOnTurnStart e_CreatureOnTurnStart;

    public delegate void CreatureOnTurnStart2();    
    public event CreatureOnTurnStart2 e_CreatureOnTurnStart2;

    public delegate void CreatureOnTurnEnd();    
    public event CreatureOnTurnEnd e_CreatureOnTurnEnd;   

    public delegate void ThisCreatureDies();    
    public event ThisCreatureDies e_ThisCreatureDies;

    public delegate void IsAttacked();    
    public event IsAttacked e_IsAttacked; 

    public delegate void IsComputeDamage();    
    public event IsComputeDamage e_IsComputeDamage;     

    public delegate void BeforeAttacking(CreatureLogic target);    
    public event BeforeAttacking e_BeforeAttacking; 
    
    public delegate void AfterAttacking(CreatureLogic target);    
    public event AfterAttacking e_AfterAttacking; 

    public delegate void SecondAttack(CreatureLogic target);    
    public event SecondAttack e_SecondAttack; 
    
    
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
        set{}
        
        
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

    private int attackDamage;
    public int AttackDamage
    {
        get {return attackDamage;}
        set{
            if(value < 0)
                attackDamage = 0;
            else
                attackDamage = value;
        }
    }


    private int criticalFactor = 1;
    public float CriticalChance
    {
        get{return criticalFactor;}
        set{
            if(value < 1)
                criticalFactor = 1;
            else
                criticalFactor = 2;
        }
    }

    private int otherFactor = 1;
    public int OtherFactor
    {
        get{return otherFactor;}
        set{otherFactor = value;}
    }

    private int damageReduction = 1;
    public int DamageReduction
    {
        get{return damageReduction;}
        set
        {
            damageReduction = value;
        }
    }

    private int otherDamageReduction = 1;
    public int OtherDamageReduction
    {
        get{return otherDamageReduction;}
        set
        {
            otherDamageReduction = value;
        }
    }

    
    //Flags
   
    [HideInInspector]
    public int healFactor = 1;
    [HideInInspector]
    public bool canDebuff = true;
    [HideInInspector]
    public bool canBuff = true;
    [HideInInspector]
    public bool canBeDebuffed = true;
    [HideInInspector]
    public bool canBeBuffed = true;
    [HideInInspector]
    public int targetAttackDamage = 0;
    [HideInInspector]
    public bool canUseAbility = true;

   

    [HideInInspector]
    public bool canBeAttacked = true;
    public bool hasTaunt = false;


    // CONSTRUCTOR
    public CreatureLogic(Player owner, CardAsset ca)
    {
        
        this.ca = ca;

        //DS
        abilities = ca.Abilities;
        equipAbility = ca.equipAbility;
        chance = ca.Chance;


        baseHealth = ca.MaxHealth;
        Health = ca.MaxHealth;
        baseAttack = ca.Attack;
        
        attacksForOneTurn = ca.AttacksForOneTurn;

        // Remove Charge
        // if (ca.Charge)
        //     AttacksLeftThisTurn = attacksForOneTurn;

        this.owner = owner;
        UniqueCreatureID = IDFactory.GetUniqueID(); 
        Name = this.GetType().Name.ToString();

       
        //DS
        //Add activator for abilities

        /* Replace with Abilities SO
        if (ca.abilityEffect != null)
        {
            foreach (AbilityEffect ae in ca.abilityEffect)
            {
                if (ae.CreatureScriptName != null && ae.CreatureScriptName != "")
                {
                    effect = System.Activator.CreateInstance(System.Type.GetType(ae.CreatureScriptName), new System.Object[]{owner, this, ae.coolDown}) as CreatureEffect;
                    effect.RegisterCooldown();
                    effect.RegisterEventEffect();

                    if (ae.abilityImage != null)
                    effect.abilityPreviewSprite = ae.abilityImage;
                    effect.abilityDescription = ae.abilityDescription;

                    creatureEffects.Add(effect);

                   
                   
                    
                }
            }
        }
        */

        if (ca.Abilities != null)
        {
            foreach (AbilityAsset ae in ca.Abilities)
            {
                if (ae.abilityEffect != null && ae.abilityEffect != "")
                {
                    effect = System.Activator.CreateInstance(System.Type.GetType(ae.abilityEffect), new System.Object[]{owner, this, ae.abilityCoolDown}) as CreatureEffect;
                    effect.RegisterCooldown();
                    effect.RegisterEventEffect();

                    if (ae.icon != null)
                    effect.abilityPreviewSprite = ae.icon;
                    effect.abilityDescription = ae.description;

                    creatureEffects.Add(effect);
 
                }
            }
        }


//for Equip
        if (ca.equipAbility != null)
        {
            if (ca.equipAbility.characterEffect != null && ca.equipAbility.characterEffect != "")
            {
                effect = System.Activator.CreateInstance(System.Type.GetType(ca.equipAbility.characterEffect), new System.Object[]{owner,this}) as CreatureEffect;
                //effect.RegisterCooldown();
                //effect.RegisterEventEffect();

                if (ca.equipAbility.icon != null)
                effect.abilityPreviewSprite = ca.equipAbility.icon;
                effect.abilityDescription = ca.equipAbility.description;

                equipEffect = effect;

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

        //TODO: Buff/Debuff effects (like Poison, Heal)   

         //TODO:  Ability Effects (BattleCry, etc.)
        foreach(CreatureEffect ce in creatureEffects)
        {
            //Debug.Log ("CreatureEffect: " + ce.ToString() + ", CD: " + ce.remainingCooldown);
        }

         if(e_CreatureOnTurnStart != null)
            e_CreatureOnTurnStart.Invoke();

        
                
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

        
        foreach(CreatureEffect ce in creatureEffects)
        {
            if(effect !=null)
            {
                ce.remainingCooldown = ce.creatureEffectCooldown;
                ce.WhenACreatureDies();
                ce.UnRegisterEventEffect();
                ce.UnregisterCooldown();                                   
            }
        }        
            
        //if(!hasResurrect)
        creatureEffects.Clear();
        

        new CreatureDieCommand(UniqueCreatureID, owner).AddToQueue();         
        

        if(e_ThisCreatureDies != null)
        e_ThisCreatureDies.Invoke();
        
        owner.CheckIfGameOver();           
    }

    public void GoFace()
    {
        AttacksLeftThisTurn--;
        int targetHealthAfter = owner.otherPlayer.Health - Attack;
        new CreatureAttackCommand(owner.otherPlayer.PlayerID, UniqueCreatureID, 0, Attack, Health, targetHealthAfter, CanAttack).AddToQueue();
        owner.otherPlayer.Health -= Attack;
    }

    public void AttackCreature (CreatureLogic target)
    {
        
        AttacksLeftThisTurn--;         

        //call this creatures after attack event
            if(this.e_BeforeAttacking != null)
            this.e_BeforeAttacking.Invoke(target);   

        //call this creatures attack event
        // if(this.e_AfterAttacking != null)
        //     this.e_AfterAttacking.Invoke(target);     
        
        // calculate the values so that the creature does not fire the DIE command before the Attack command is sent        
        
        //original
        //AttackDamage = Attack*criticalFactor;
        
        //TEST
        //DS:  DealDamage
        AttackDamage = DealDamage(Attack);
        
        //target.targetAttackDamage = target.targetAttackDamage*criticalFactor;

        //TEST
        //DS:target.DealDamage
        target.targetAttackDamage = target.DealDamage(targetAttackDamage);
        

        //DS:  replace with target.TakeDamage
        //int targetHealthAfter = target.Health - AttackDamage;
        int targetHealthAfter = target.TakeDamageVisual(AttackDamage);


        if(targetHealthAfter > target.MaxHealth)
        targetHealthAfter = target.MaxHealth;


        //original
        //int attackerHealthAfter = Health - target.Attack;

        //replace with, TakeDamage
        //int attackerHealthAfter = Health - target.targetAttackDamage;
        int attackerHealthAfter = TakeDamageVisual(target.targetAttackDamage);

        if(Health > MaxHealth)
        attackerHealthAfter = MaxHealth;

        //original
        //new CreatureAttackCommand(target.UniqueCreatureID, UniqueCreatureID, target.Attack, Attack, attackerHealthAfter, targetHealthAfter).AddToQueue();

        //set target attack to 0 to reflect non-damage for the attacker
        new CreatureAttackCommand(target.UniqueCreatureID, UniqueCreatureID, target.targetAttackDamage, AttackDamage, attackerHealthAfter, targetHealthAfter, CanAttack).AddToQueue();

        //Target's Health minus my attack damage
        //DS:  target.TakeDamage;
        //target.Health -= AttackDamage;
        target.TakeDamage(AttackDamage);

        //My Health minus my target's attack damage
        //TakeDamage;
        TakeDamage(target.targetAttackDamage);
        
        //originally enabled 
        //Health -= target.Attack;       

        //call enemy targets Is Attacked event
        if(target.e_IsAttacked != null)
            target.e_IsAttacked.Invoke();      
       
        
        //DS
        //implement silence here
        // foreach(CreatureEffect ce in creatureEffects)
        // {
        //     //Debug.Log ("CreatureEffect: " + ce.ToString() + ", CD: " + ca.specialSpellAmount);
        //     if(ce.creatureEffectCooldown == 0)
        //     {
        //         Debug.Log ("Using CreatureEffect: " + ce.ToString());
        //         ce.UseEffect(target);

        //     }
        // }
        
        //call this creatures after attack event
            if(this.e_AfterAttacking != null)
            this.e_AfterAttacking.Invoke(target);  
       
        if(!CanAttack)
        {
            
            //implement command here
            //TurnManager.Instance.EndTurn();

            //DS
            new EndTurnCommand().AddToQueue();

        }else{            
            if(e_SecondAttack != null)
            e_SecondAttack.Invoke(target);      
        }
        
           
        
    }//Attack Creature   

    public void AttackCreatureWithID(int uniqueCreatureID)
    {
        CreatureLogic target = CreatureLogic.CreaturesCreatedThisGame[uniqueCreatureID];
        AttackCreature(target);
    }


    public void SplashAttackDamage(CreatureLogic target, int splashDamage)
    {
        List<CreatureLogic> enemies = owner.EnemyList();

            foreach(CreatureLogic enemy in enemies)
            {
                
                if(enemy != target)
                {
                     //new DelayCommand(0.5f).AddToQueue();    
                
                    

                        int damageTakenByTarget = splashDamage;
                        // GameObject targetEnemy = IDHolder.GetGameObjectWithID(enemy.UniqueCreatureID);    

                    	// if(damageTakenByTarget>0) 
	                    // DamageEffect.CreateDamageEffect(targetEnemy.transform.position, damageTakenByTarget);

                        new SplashDamageCommand(enemy.UniqueCreatureID, damageTakenByTarget).AddToQueue();
	                    

                    //enemy.TakeDamage(creature.AttackDamage);      
                    enemy.TakeDamage(splashDamage);      
                   
                   new UpdateHealthCommand(enemy.UniqueCreatureID, enemy.Health).AddToQueue();
                }                
                               
            }         

            enemies.Clear();
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

                new UpdateBuffCommand(be).AddToQueue();
            }
        }

        //if buff is new, add it to the CL

        if (!buffExists)
        {
            buff.RegisterCooldown();
            
            buff.CauseBuffEffect();
            buffEffects.Add(buff);

           new AddBuffCommand(buff, UniqueCreatureID).AddToQueue();
        }
        

    }   

    

   //Remove Buff is in BuffEffect Script

   public void RemoveRandomBuff()
    {
        var randList = new List<BuffEffect>();

        foreach(BuffEffect be in buffEffects)
        {
            if(be.isBuff)
            randList.Add(be);
        }

        if(randList.Count>=1)
        {
            BuffEffect buff = randList[Random.Range(0,randList.Count)];
            buff.UndoBuffEffect();
            buff.UnregisterCooldown();
            buffEffects.Remove(buff);

            new DestroyBuffCommand(buff, this.UniqueCreatureID).AddToQueue();

        }
       
    }

    public void RemoveAllBuffs()
    {
        if(buffEffects != null)
        foreach (BuffEffect be in buffEffects)
        {          
            be.UndoBuffEffect();                
            be.UnregisterCooldown();                         
        }
        buffEffects.Clear();
    }//RemoveAllBuffs

    public void RemoveDeBuffsAll()
    {
        int i = this.buffEffects.Count;
        int buffCounter = 0;
        for(int x = i-1; x>=0; x--)
        {
            if(this.buffEffects[x].isDebuff)
            {
                this.buffEffects[x].RemoveBuff();
                buffCounter++;
            }            
        }
    }

    public void RemoveBuffsAll()
    {
        int i = this.buffEffects.Count;
        int buffCounter = 0;
        for(int x = i-1; x>=0; x--)
        {
            if(this.buffEffects[x].isBuff)
            {
                this.buffEffects[x].RemoveBuff();
                buffCounter++;
            }            
        }
    }

    

    
    public void Heal(int amount)
    {
        ChangeHealth(amount, healFactor);
    }

    
    // //Used to compute Damage this creature takes
    // public void TakeDamage(int amount, CreatureLogic source)
    // {
    //     int damage = amount*DamageReduction;

    //     new DelayCommand(0.5f).AddToQueue();
    //     new DealDamageCommand(target.ID, poisonDamage, healthAfter: target.Health - target.ComputeDamage(poisonDamage, target)).AddToQueue();

    //     target.Health -= target.ComputeDamage(poisonDamage, target);    
    //     Debug.Log("Poison Activated" +target.UniqueCreatureID);

        
    // }  
    
    
    
    // //Used to compute Damage this creature deals
    // public int ComputeDamage(int amount, CreatureLogic target)
    // {
    //     int damage = amount*criticalFactor;

    //     if(e_IsComputeDamage!=null)
    //     e_IsComputeDamage();

    //     return damage;
    // }

    //used by attack based damage
    public int DealDamage(int amount)
    {
        int damage = amount*criticalFactor;

        if(e_IsComputeDamage!=null)
        e_IsComputeDamage();

        return damage;
    }

    //used by non-attack based damage
    public int DealOtherDamage(int amount)
    {
        int damage = amount*OtherFactor;

        if(e_IsComputeDamage!=null)
        e_IsComputeDamage();

        return damage;
    }

    //input: DealDamage
    public void TakeDamage(int damage)
    {
              
        int finalDamage = DamageReduction*damage;
        Health-=finalDamage;
    }

    

    public int TakeDamageVisual(int damage)
    {
        int finalDamage = DamageReduction*damage;
        int healthAfter = Health;
        healthAfter-=finalDamage;
        return healthAfter;
    }


    // for non-attack damage
    public void TakeOtherDamage(int damage)
    {
              
        int finalOtherDamage = OtherDamageReduction*damage;
        Health-=finalOtherDamage;
    }

    public int TakeOtherDamageVisual(int damage)
    {
        int finalOtherDamage = OtherDamageReduction*damage;
        int healthAfter = Health;
        healthAfter-=finalOtherDamage;
        return healthAfter;
    }


  
    
    
    public void ChangeHealth(int changeAmount, int factor)
    {    
       int amount = changeAmount*factor; 

       int healthAfter = Health + amount;
       if(healthAfter > MaxHealth)
            healthAfter = MaxHealth;
            
       
      
       new DelayCommand(0.5f).AddToQueue();
       if(amount>=0)
        {          
            new DealHealingCommand(this.ID, amount, healthAfter).AddToQueue();           

        }
        else if(amount < 0)
        {           
            new DealDamageCommand(this.ID, -amount, healthAfter).AddToQueue();            
        }
        
        Health += amount;
    }

    
    // STATIC For managing IDs
    public static Dictionary<int, CreatureLogic> CreaturesCreatedThisGame = new Dictionary<int, CreatureLogic>();

}
