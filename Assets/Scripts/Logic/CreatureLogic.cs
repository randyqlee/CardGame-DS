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
    public int UniqueCreatureID;

    public int defaultSpeed = 100;

    //storage for creature effects    
    public List<CreatureEffect> creatureEffects = new List<CreatureEffect>(); 

    public List<CreatureEffect> equipEffects = new List<CreatureEffect>();

    public List<BuffEffect> buffEffects = new List<BuffEffect>(); 

    // delegates


    public bool isDead;

    public bool isActive; 

    public bool IsActive
    {
        get {return isActive;}
        set
        {
            isActive = value;
            if (!isActive)
            {
                new CreatureColorCommand(this,true).AddToQueue();
            }
            else
                new CreatureColorCommand(this,false).AddToQueue();


        }
    } 

    public bool isValidTarget;
    public bool IsValidTarget
    {
        get {return isValidTarget;}
        set
        {
            isValidTarget = value;
            
            GameObject g = IDHolder.GetGameObjectWithID(UniqueCreatureID);
            if(g!= null)
            //DS
            {
                g.GetComponent<OneCreatureManager>().IsValidTarget = value;
            }
        }
    }

    [HideInInspector]
    //public BuffEffect testBuff;

    public int chance;

    public bool isEquip = false;



    public delegate void VoidWithNoArguments();
    public event VoidWithNoArguments e_CreatureOnTurnStart;
    public event VoidWithNoArguments e_CreatureOnTurnEnd;   
    public event VoidWithNoArguments e_IsComputeDamage;     


    public delegate void VoidWithCL(CreatureLogic cl);
    public event VoidWithCL e_PreAttackEvent;
    public event VoidWithCL e_BeforeAttacking; 
    public event VoidWithCL e_PreDealDamage;
    public event VoidWithCL e_AfterAttacking; 
    public event VoidWithCL e_SecondAttack; 
    public event VoidWithCL e_IsAttacked;
    public event VoidWithCL e_IsAttackedBy;
    public event VoidWithCL e_ThisCreatureDies;
    public event VoidWithCL e_IsValidTarget;

    public delegate void VoidWithCL_CL_int(CreatureLogic source, CreatureLogic target, int damage);    
    public event VoidWithCL_CL_int e_IsDamagedByAttack;

    public delegate void VoidWithCL_BE(CreatureLogic Target, BuffEffect buff);    
    public event VoidWithCL_BE e_buffApplied;     

    
    // PROPERTIES
    // property from ICharacter interface
    public int ID
    {
        get{ return UniqueCreatureID; }
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
            //else if (value <= 0)                
                //Die();
            else
                health = value;
        }
    }

    private int speed;
    
    public int Speed
    {
        get{ return speed; }

        set
        {                 
            speed = value;
        }
    }

    private int energy;
    
    public int Energy
    {
        get{ return energy; }

        set
        {                 
            energy = value;
        }
    }
        
    // the basic health that we have in CardAsset
    private int baseHealth;
    // health with all the current buffs taken into account
    public int MaxHealth
    {
        get{ return baseHealth;}
    }

    private int armor;
    public int Armor
    {
        get{ return armor; }
        set
        {
            if (value <= 0)
            {
                armor = 0;
                value = armor;                 
            }                             
            else
            {
                armor = value;
            }
            new UpdateArmorCommand(ID, value).AddToQueue();                         
        }
    }

    // returns true if we can attack with this creature now
    public bool CanAttack
    {
        get
        {
            bool ownersTurn = (TurnManager.Instance.whoseTurn == owner);
            return (ownersTurn && (AttacksLeftThisTurn > 0));
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
    public int attacksForOneTurn = 1;
    private int attacksLeftThisTurn;
    public int AttacksLeftThisTurn
    {
        get{return attacksLeftThisTurn;}
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
            if(value <= 1)
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

    private int damageReductionAdditive = 0;
    public int DamageReductionAdditive
    {
        get{return damageReductionAdditive;}
        set
        {
            damageReductionAdditive = value;
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
    public int attackTurnModifier;

   

    [HideInInspector]
    public bool canBeAttacked = true;
    public bool hasTaunt = false;

    public bool hasBless = false;

    public bool hasEndure = false;

    public float chanceTakeDamageFromAttack = 100;

    public int lastDamageValue;

    public bool hasHorror = false;

    public bool isPrimaryForm = true;

    public bool hasCurse = false;

    public bool pauseAttack = false;
    public bool hasStun = false;

    public bool extraTurn = false;

    public ATBTimer timer;


    // CONSTRUCTOR
    public CreatureLogic(Player owner, CardAsset ca)
    {

        this.ca = ca;

        chance = ca.Chance;

        baseHealth = ca.MaxHealth;
        Health = ca.MaxHealth;
        baseAttack = ca.Attack;
        
        attacksForOneTurn = ca.AttacksForOneTurn;


        this.Speed = ca.Speed;
        if (this.Speed == 0)
            this.Speed = Random.Range(1,6)*100;

        // Remove Charge
        // if (ca.Charge)
        //     AttacksLeftThisTurn = attacksForOneTurn;

        this.owner = owner;
        UniqueCreatureID = IDFactory.GetUniqueID(); 
        //Name = this.GetType().Name.ToString();
        Name = ca.cardName;

        this.timer = new ATBTimer(this,Speed);
        


        //attach ability scripts to CL
        if (ca.Abilities != null)
        {
            foreach (AbilityAsset ae in ca.Abilities)
            {
                if (ae.abilityEffect != null && ae.abilityEffect != "")
                {
                    
                    CreatureEffect effect = System.Activator.CreateInstance(System.Type.GetType(ae.abilityEffect), new System.Object[]{owner, this, ae.abilityCoolDown}) as CreatureEffect;
                    
                    //effect.RegisterCooldown();
                    //effect.RegisterEventEffect();

                    if (ae.icon != null)
                    effect.abilityPreviewSprite = ae.icon;
                    effect.abilityDescription = ae.description;
                    effect.skillType = ae.skillType;

                    creatureEffects.Add(effect);
 
                }
            }
        }

        CreaturesCreatedThisGame.Add(UniqueCreatureID, this);

        //initialize creature effects

        foreach (CreatureEffect ce in creatureEffects)
        {
            ce.RegisterCooldown();
            ce.RegisterEventEffect();

            //DS Test: 24 Nov 2019. Initialize Ultimate Skill CD to 1.  
            //if(ce.skillType == SkillType.Ultimate)
            //{
            //    ce.remainingCooldown = 1;
            //    new UpdateCooldownCommand(ce.abilityCard, ce.remainingCooldown, ce.creatureEffectCooldown).AddToQueue();
            //}
            
        }


        //attach EQUIP ability scripts to CL
        if (ca.EquipAbilities != null)
        {
            foreach (AbilityAsset ae in ca.EquipAbilities)
            {
                if (ae.abilityEffect != null && ae.abilityEffect != "")
                {
                    
                    CreatureEffect effect = System.Activator.CreateInstance(System.Type.GetType(ae.abilityEffect), new System.Object[]{owner, this, ae.abilityCoolDown}) as CreatureEffect;
                    
                    //effect.RegisterCooldown();
                    //effect.RegisterEventEffect();

                    if (ae.icon != null)
                    effect.abilityPreviewSprite = ae.icon;
                    effect.abilityDescription = ae.description;
                    effect.skillType = ae.skillType;

                    equipEffects.Add(effect);
 
                }
            }
        }


    }

    
    // METHODS
    public void OnTurnStart()
    {
        IsActive = true;
        extraTurn = false;
        AttacksLeftThisTurn = attacksForOneTurn + attackTurnModifier; 
        IsValidTarget = false;
    }

    public void OnTurnEnd()
    {
        if(!extraTurn)
        {
            if (isActive)
            {
                IsActive = false;
                AttacksLeftThisTurn = 0;
                //change to command
                //timer.ResetTurn();
                new TimerResetCommand(this).AddToQueue();
            }
        }
        else
        {
            IsActive = true;
            if (AttacksLeftThisTurn == 0)
                AttacksLeftThisTurn++;
            extraTurn = false;
        }

        if(e_CreatureOnTurnEnd != null)
            e_CreatureOnTurnEnd.Invoke();              
    }

    public void DestroyArmor()
    {
         if(Armor <=0)
            {
                for(int i = this.buffEffects.Count-1; i>=0; i--)
                {
                    Debug.Log("Destroy Armor Hero Name : " +this.Name);
                    if(this.buffEffects[i].Name == "Armor")
                    {
                         Debug.Log("Command Buff: " +this.buffEffects[i].Name);
                         BuffSystem.Instance.RemoveBuff(this,this.buffEffects[i]);
                    }
                    
                }
            }
    }

    public void Die()
    {   
        //ORIGINAL SCRIPT
        //owner.table.CreaturesOnTable.Remove(this);        
        
        //New SCRIPT
      
        this.isDead = true;

        this.IsActive = false;

        
        //Remove all buffs/debuffs        
        BuffSystem.Instance.RemoveAllBuffs(this); 

        
        foreach(CreatureEffect ce in creatureEffects)
        {
            //if(effect !=null)
            //{
                ce.remainingCooldown = ce.creatureEffectCooldown; 

                ce.WhenACreatureDies();
                ce.UnRegisterEventEffect();
                ce.UnregisterCooldown();                                   
            //}
        }        
            
        //if(!hasResurrect)
        
        //REMOVE INSTANTIATED EFFECTS
        //creatureEffects.Clear();
        

        new CreatureDieCommand(UniqueCreatureID, owner).AddToQueue();         
        

        if(e_ThisCreatureDies != null)
        e_ThisCreatureDies.Invoke(this);
        
        //owner.CheckIfGameOver();           
    }

    public void Revive()
    {
        if (!hasCurse)
        {
            isDead = false;
            OnTurnStart();        

            foreach(CreatureEffect ce in creatureEffects)
            {
                //if(effect !=null)
                //{
                    ce.remainingCooldown = ce.creatureEffectCooldown;
                    ce.RegisterCooldown();
                    ce.RegisterEventEffect();                                
                //}
            }            
            new CreatureResurrectCommand(UniqueCreatureID, owner).AddToQueue(); 
            new UpdateAttackCommand(ID, Attack).AddToQueue();
            new UpdateHealthCommand(ID, MaxHealth).AddToQueue();
            new UpdateArmorCommand(ID, Armor).AddToQueue();
            new CreatureColorCommand(this,false).AddToQueue();
        }
    }

    public void AttackCreature (CreatureLogic target)
    {
        lastDamageValue = 0;        
        AttacksLeftThisTurn--;         
        if(!target.isDead)
        {
            AttackDamage = DealDamage(Attack);          
            
            if (Random.Range(0,100) <= target.chanceTakeDamageFromAttack)
            {          
                target.TakeDamage(this, AttackDamage);
            } 
            else if (Random.Range(0,100) > target.chanceTakeDamageFromAttack)
            {
                AttackDamage = 0;
                target.TakeDamage(this, AttackDamage);
            }

            int targetHealthAfter = target.Health;
            int attackerHealthAfter = Health;
            int targetArmorAfter = target.Armor;
            new CreatureAttackCommand(target.UniqueCreatureID, UniqueCreatureID, target.targetAttackDamage, AttackDamage, attackerHealthAfter, targetHealthAfter, targetArmorAfter, CanAttack).AddToQueue();
            
            if(this.e_AfterAttacking != null)
            this.e_AfterAttacking.Invoke(target);

            if (target.lastDamageValue > 0)
            {
                if(e_IsDamagedByAttack != null)
                    e_IsDamagedByAttack.Invoke(this, target, target.lastDamageValue); 
                
                target.TriggerThisWhenAttacked(this);

            }
        
            if(!CanAttack)
            {
                //new EndTurnCommand().AddToQueue();
                OnTurnEnd();
            }
            else
            {            
                if(e_SecondAttack != null)
                e_SecondAttack.Invoke(target);    
            }   

        } else {
             if(!CanAttack)
            {
                //new EndTurnCommand().AddToQueue();
                OnTurnEnd();
            }
            else
            {            
                if(e_SecondAttack != null)
                e_SecondAttack.Invoke(target);    
            }   
        }
    } 

    public void TriggerThisWhenAttacked (CreatureLogic attacker)
    {
        if(e_IsAttackedBy != null)
            e_IsAttackedBy.Invoke(attacker);
    }

    public int DealDamage(int amount)
    {
        int damage = amount*criticalFactor;
 
        //if(e_IsComputeDamage!=null)
        //e_IsComputeDamage();

        return damage;
    }

    //used by non-attack based damage
    public int DealOtherDamage(int amount)
    {
        int damage = amount*OtherFactor;
        return damage;
    }
    
    public void TakeDamage(CreatureLogic source, int damage)
    {

        if(e_PreDealDamage != null)
            e_PreDealDamage.Invoke(source); 
        
        
        if(!hasBless)
        {
            if(hasHorror && source.CriticalChance == 1)
                damage = damage * 2;
            
            int finalDamage = DamageReduction*damage - DamageReductionAdditive;
            int healthAfter = Health;
            int spillDamage = 0;
            
            if (Armor > 0)
            {
                Debug.Log("Armor: " + Armor);
                Debug.Log("Final Damage: " + finalDamage);
                if (Armor > finalDamage)
                {
                    Armor -= finalDamage;
                }
                else if (Armor == finalDamage)
                {
                    Armor = 0;
                    //source.DestroyArmor();
                    this.DestroyArmor();
                }
                else if (Armor < finalDamage)
                {
                    spillDamage = Armor- finalDamage;                    
                    Armor = 0;
                    //source.DestroyArmor();
                    this.DestroyArmor();
                    healthAfter += spillDamage;
                }
            }

            else
            {
               
                healthAfter-=finalDamage;
            }

            lastDamageValue = finalDamage;

            if (healthAfter <= 0 && hasEndure)
            {
                Health = 1;
            }
            else
                Health = healthAfter;

            

            if(e_IsAttacked != null)
            e_IsAttacked.Invoke(this); 

            if(e_IsComputeDamage!=null && (damage - DamageReductionAdditive) > 0)
            e_IsComputeDamage.Invoke();



        }
    }

    public void TakeSplashDamage(CreatureLogic source, int damage)
    {
  
        if(!hasBless)
        {
            if(hasHorror && source.CriticalChance == 1)
                damage = damage * 2;
            
            int finalDamage = DamageReduction*damage - DamageReductionAdditive;
            int healthAfter = Health;
            int spillDamage = 0;
            
            if (Armor > 0)
            {
                Debug.Log("Armor: " + Armor);
                Debug.Log("Final Damage: " + finalDamage);
                if (Armor > finalDamage)
                {
                    Armor -= finalDamage;
                }
                else if (Armor == finalDamage)
                {
                    Armor = 0;
                    this.DestroyArmor();
                }
                else if (Armor < finalDamage)
                {
                    spillDamage = Armor - finalDamage;                    
                    Armor = 0;
                    this.DestroyArmor();
                    healthAfter += spillDamage;
                }
            }

            else
            {
                healthAfter-=finalDamage;
            }

            lastDamageValue = finalDamage;

            if (healthAfter <= 0 && hasEndure)
            {
                Health = 1;
            }
            else
                Health = healthAfter;

            if(e_IsComputeDamage!=null && (damage - DamageReductionAdditive) > 0)
            e_IsComputeDamage.Invoke();



        }
    }

    // for non-attack damage
    public void TakeOtherDamage(int damage)
    {

        int finalOtherDamage = OtherDamageReduction*damage - DamageReductionAdditive;
        int healthAfter = Health;
        int spillDamage = 0;
        
        if (Armor > 0)
        {
            if (Armor > finalOtherDamage)
            {
                Armor -= finalOtherDamage;
            }
            else if (Armor == finalOtherDamage)
            {
                Armor = 0;
                this.DestroyArmor();
            }
            else if (Armor < finalOtherDamage)
            {
                spillDamage = Armor - finalOtherDamage;
                Armor = 0;
                this.DestroyArmor();
                healthAfter += spillDamage; 
            }
        }

        else
        {
            healthAfter-=finalOtherDamage;
        }

        lastDamageValue = finalOtherDamage;

        if (healthAfter <= 0 && hasEndure)
            {
                Health = 1;
            }
        else
            Health = healthAfter;

        if(e_IsComputeDamage!=null && (damage - DamageReductionAdditive) > 0)
            e_IsComputeDamage.Invoke();

    }


    //DS: TO BE REVIEWED
    public int TakeOtherDamageVisual(int damage)
    {
        int finalOtherDamage = OtherDamageReduction*damage - DamageReductionAdditive;
        int healthAfter = Health;
        int spillDamage = 0;
        int armorValue = Armor;
        
        if (armorValue > 0)
        {
            if (armorValue > finalOtherDamage)
            {
                armorValue -= finalOtherDamage;
            }
            else if (armorValue == damage)
            {
                armorValue = 0;
                this.DestroyArmor();
                Debug.Log("Hero: " +this.Name);
            }
            else if (Armor < finalOtherDamage)
            {
                spillDamage = armorValue - finalOtherDamage;
                armorValue = 0;
                this.DestroyArmor();
                Debug.Log("Hero: " +this.Name);
                healthAfter += spillDamage; 
            }
        }

        else
        {
            healthAfter-=finalOtherDamage;
        }

        if (healthAfter <= 0 && hasEndure)
            {
                return 1;
            }
        else
            return healthAfter;
    }

    public int TakeArmorDamageVisual(int damage)
    {
        int finalOtherDamage = OtherDamageReduction*damage - DamageReductionAdditive;
        int healthAfter = Health;
        int spillDamage = 0;
        int armorValue = Armor;
        
        if (armorValue > 0)
        {
            if (armorValue > finalOtherDamage)
            {
                armorValue -= finalOtherDamage;
            }
            else if (armorValue == damage)
            {
                armorValue = 0;
                Debug.Log("Hero Armor: " +this.Name);
                this.DestroyArmor();
            }
            else if (Armor < finalOtherDamage)
            {
                spillDamage = armorValue - finalOtherDamage;
                armorValue = 0;
                Debug.Log("Hero Armor: " +this.Name);
                this.DestroyArmor();
                healthAfter += spillDamage; 
            }

            return armorValue;
        }

        else
            return 0;
    }

    public void AttackCreatureWithID(int uniqueCreatureID)
    {
        
        CreatureLogic target = CreatureLogic.CreaturesCreatedThisGame[uniqueCreatureID];
        new StartPreAttackSequenceCommand(this, target).AddToQueue();
        

        //PreAttack(target);
        //AttackCreature(target);        
        
    }


    public void PreAttack(CreatureLogic target)
    {
        //Subscribe all creature pre-attack abilities here
        if(e_PreAttackEvent != null)
           this.e_PreAttackEvent.Invoke(target);
           new DelayCommand(0.5f).AddToQueue();
            
           

        if(!pauseAttack)
            new StartAttackSequenceCommand(this, target).AddToQueue();
        else
        {
            pauseAttack = false;
        }


    }

    public void SplashAttackDamage(CreatureLogic target, int splashDamage)
    {
        List<CreatureLogic> enemies = owner.EnemyList();

            foreach(CreatureLogic enemy in enemies)
            {
                
                if(enemy != target && !enemy.isDead)
                {
                     //new DelayCommand(0.5f).AddToQueue();    
                
                    

                        int damageTakenByTarget = splashDamage;
                        // GameObject targetEnemy = IDHolder.GetGameObjectWithID(enemy.UniqueCreatureID);    

                    	// if(damageTakenByTarget>0) 
	                    // DamageEffect.CreateDamageEffect(targetEnemy.transform.position, damageTakenByTarget);

                        new SplashDamageCommand(enemy.UniqueCreatureID, damageTakenByTarget).AddToQueue();
	                    

                    //enemy.TakeDamage(creature.AttackDamage);      
                    enemy.TakeSplashDamage(this, splashDamage);      
                   
                   new UpdateHealthCommand(enemy.UniqueCreatureID, enemy.Health).AddToQueue();
                }                
                               
            }         

            enemies.Clear();
    }

    public void E_buffApplied (BuffEffect buff)
    {
        if(e_buffApplied!=null)
            e_buffApplied.Invoke(this, buff);
    }  

    

   //Remove Buff is in BuffEffect Script

    public BuffEffect RandomBuff()
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
            return buff;
        }

        else return null;

        
    }

    public BuffEffect RandomDebuff()
    {
        var randList = new List<BuffEffect>();

        foreach(BuffEffect be in buffEffects)
        {
            if(be.isDebuff)
            randList.Add(be);
        }

        if(randList.Count>=1)
        {
            BuffEffect buff = randList[Random.Range(0,randList.Count)];
            return buff;
        }
        else return null;

        
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

  
    
    
    public void ChangeHealth(int changeAmount, int factor=1)
    {    
       int amount = changeAmount*factor; 

       int healthAfter = Health + amount;
       if(healthAfter > MaxHealth)
            healthAfter = MaxHealth;
            
       
      
       new DelayCommand(0.5f).AddToQueue();
       if(amount>=0)
        {
            Debug.Log("healthAfter: " + healthAfter);          
            new DealHealingCommand(this.ID, amount, healthAfter).AddToQueue();
            

        }
        else if(amount < 0)
        {           
            new DealDamageCommand(this.ID, -amount, healthAfter, Armor).AddToQueue();   
            
        }

        new UpdateHealthCommand(ID, healthAfter).AddToQueue();        
        Health += amount;
    }

    public void ExtraTurn()
    {
        extraTurn = true;
    }

    public void ExtraAttack()
    {
        AttacksLeftThisTurn++;
    }

    public void Transform()
    {
        //transform mechanic;
    }


    
    // STATIC For managing IDs
    public static Dictionary<int, CreatureLogic> CreaturesCreatedThisGame = new Dictionary<int, CreatureLogic>();

}
