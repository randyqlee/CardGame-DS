using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

//DS
public class CreatureEffect
//public abstract class CreatureEffect
{
    //protected Player owner;
    //protected CreatureLogic creature;
    //protected int creatureEffectCooldown;

    //DS
    
    public Player owner;   
     
    public string Name;

    public Sprite abilityPreviewSprite;
    public string abilityDescription;

    public AbilityCard abilityCard;

    public SkillType skillType;

    [HideInInspector]
    public CreatureLogic creature;

   [HideInInspector]
    public bool cooldownCantChange = false;
    
   

    //DS
    public int creatureEffectCooldown;

//    public int remainingCD;
    public int remainingCooldown;
//    {
//        get {return remainingCD;}
//        set
//        {
//            remainingCD = value;
//            UpdateCooldown();
//        }
//    }
    public bool hasUsedEffect = false;

    public bool isCooldownPaused = false;
    
    
    //Events
    public delegate void AddBuffEvent();    
    public event AddBuffEvent e_AddBuffEvent;   
   
    public CreatureEffect(Player owner, CreatureLogic creature, int creatureEffectCooldown)
    {
        this.creature = creature;
        this.owner = owner;
        this.creatureEffectCooldown = creatureEffectCooldown;
        
        
        remainingCooldown = creatureEffectCooldown;      


        Name = this.GetType().Name.ToString();
        
    }


    public CreatureEffect(Player owner)
    {

        this.owner = owner;
        Name = this.GetType().Name.ToString();
        
    }

    public CreatureEffect(Player owner, CreatureLogic creature)
    {
        this.creature = creature;
        this.owner = owner;
        Name = this.GetType().Name.ToString();
        
    }

    // METHODS FOR SPECIAL FX THAT LISTEN TO EVENTS
    public virtual void RegisterEventEffect()
    {
        
    }

    public virtual void RegisterAuraEffect()
    {
        
    }

    public virtual void UnRegisterEventEffect(){}

    public virtual void CauseEventEffect(){}

    // BATTLECRY
    public virtual void WhenACreatureIsPlayed(){
        
    }

    // DEATHRATTLE
    public virtual void WhenACreatureDies(){}

    public virtual void RegisterCooldown()
    {
        //creature.e_CreatureOnTurnStart += ReduceCreatureEffectCooldown;
        TurnManager.Instance.e_ResetRound += ReduceCreatureEffectCooldown;
        //creature.e_CreatureOnTurnEnd += ResetCreatureEffectCooldown;
         TurnManager.Instance.e_ResetRound += ResetCreatureEffectCooldown;
    }

    public virtual void UnregisterCooldown()
    {
        //creature.e_CreatureOnTurnStart -= ReduceCreatureEffectCooldown;
        TurnManager.Instance.e_ResetRound -= ReduceCreatureEffectCooldown;
        //creature.e_CreatureOnTurnEnd -= ResetCreatureEffectCooldown;
         TurnManager.Instance.e_ResetRound -= ResetCreatureEffectCooldown;
    }

    public void ReduceCreatureEffectCooldown()
    {       
        if(remainingCooldown > 0 && !isCooldownPaused){
            remainingCooldown--;
            new UpdateCooldownCommand (this.abilityCard, remainingCooldown, creatureEffectCooldown).AddToQueue();
        }      
        //ResetCreatureEffectCooldown();      
    }

    public void SkillReduceCreatureEffectCooldown()
    {       
        if(remainingCooldown > 0 && !isCooldownPaused && !cooldownCantChange)
        {
            remainingCooldown--;
            new UpdateCooldownCommand (this.abilityCard, remainingCooldown, creatureEffectCooldown).AddToQueue();
        }            
    }

    public void SkillRefreshCreatureEffectCooldown()
    {       
        if(!cooldownCantChange)
        {
            remainingCooldown = 0;
            new UpdateCooldownCommand (this.abilityCard, remainingCooldown, creatureEffectCooldown).AddToQueue();
        }    
            
    }

    public virtual void UpdateCooldown ()
    {
        new UpdateCooldownCommand (this.abilityCard, remainingCooldown, creatureEffectCooldown).AddToQueue();
    }

    public void ResetCreatureEffectCooldown()
    {       
        if(remainingCooldown<=0 && creature.canUseAbility)
        {
            if(hasUsedEffect)
            {
                remainingCooldown = creatureEffectCooldown;
                hasUsedEffect = false; 
            }           
            //don't reset cooldown if creature has not used effect
            else if(!hasUsedEffect)
                remainingCooldown = 0;
        }
        //this is for silence scenario
        else if(remainingCooldown<=0 && !creature.canUseAbility)
        {
            remainingCooldown = 0;
        }

        new UpdateCooldownCommand (this.abilityCard, remainingCooldown, creatureEffectCooldown).AddToQueue();
            
    }

    public virtual bool ChanceOK (int chance)
    {
        if(Random.Range(0,100)<=chance)
            return true;

        else
            return false;

    }

    public virtual bool CanUseAbility ()
    {
        if(remainingCooldown <=0)
            return true;

        else
            return false;

    }

    public static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0,A.Length));
        return V;
    }

   
   

    //DS
    //Use ability overrides
    public virtual void UseEffect(CreatureLogic target)
    {
        hasUsedEffect = true;
    }

    public virtual void UseEffect(int uniqueCreatureID)
    {
        hasUsedEffect = true;
    }

    public virtual void UseEffect()
    {
           
            hasUsedEffect = true;

    }

    

    public virtual void AddBuff(CreatureLogic target, string buffName, int buffCooldown)
    {

        BuffSystem.Instance.AddBuff(this.creature, target, buffName, buffCooldown);
       
    }

    public void RemoveBuff(CreatureLogic creature, BuffEffect buff)
    {

        BuffSystem.Instance.RemoveBuff(creature, buff);
       
    }

    public void RemoveRandomBuff(CreatureLogic creature)
    {

        BuffSystem.Instance.RemoveRandomBuff(creature);
       
    }

    public void RemoveRandomDebuff(CreatureLogic creature)
    {

        BuffSystem.Instance.RemoveRandomDebuff(creature);
       
    }

    public void RemoveAllBuffs(CreatureLogic creature)
    {

        BuffSystem.Instance.RemoveAllBuffs(creature);
       
    }

    public void RemoveDeBuffsAll(CreatureLogic creature)
    {

        BuffSystem.Instance.RemoveDeBuffsAll(creature);
       
    }

    public void RemoveBuffsAll(CreatureLogic creature)
    {

        BuffSystem.Instance.RemoveBuffsAll(creature);
       
    }


    public virtual void ShowAbility()
    {
        
        if(remainingCooldown <= 0)
        {
            GameObject Target = IDHolder.GetGameObjectWithID(creature.UniqueCreatureID);
		    
            //Target.GetComponent<OneCreatureManager>().overheadText.GetComponent<OverheadText>().FloatingText(this.ToString());
            //new UseAbilityFloatingTextCommand(this.ToString(), Target.GetComponent<IDHolder>().UniqueID).AddToQueue();

            
          
            new ShowSkillPreviewCommand(this, Target.GetComponent<IDHolder>().UniqueID, this.ToString()).AddToQueue();         
            
            
            new SkillSFXCommand(Target.GetComponent<IDHolder>().UniqueID, SkillSFXCommand.SFXStates.UseSkill).AddToQueue();

            //delay for text dispalys
            new DelayCommand(1f).AddToQueue();
        }
        
        
    }

    //can be used in the future for floatnig text for the target enemy as well
    public virtual void ShowAbility(CreatureLogic target)
    {
        
        if(remainingCooldown <= 0)
        {
            GameObject Target = IDHolder.GetGameObjectWithID(creature.UniqueCreatureID);
            
		    
            //Target.GetComponent<OneCreatureManager>().overheadText.GetComponent<OverheadText>().FloatingText(this.ToString());
            //new UseAbilityFloatingTextCommand(this.ToString(), Target.GetComponent<IDHolder>().UniqueID).AddToQueue();

            
            new ShowSkillPreviewCommand(this, Target.GetComponent<IDHolder>().UniqueID, this.ToString()).AddToQueue();

            new SkillSFXCommand(Target.GetComponent<IDHolder>().UniqueID, SkillSFXCommand.SFXStates.UseSkill).AddToQueue();

            //delay for text dispalys
            new DelayCommand(1f).AddToQueue();
           
        }
        
        
    }

    public int TotalChance(int effectChance)
    {
        int totalChance;

        totalChance = effectChance + creature.chance;

        return totalChance;
    }

    public virtual void DealDamageEffect(CreatureLogic target, int damage)
    {
        new DealDamageCommand(target.ID, damage, healthAfter: target.TakeOtherDamageVisual(damage), armorAfter:target.TakeArmorDamageVisual(damage)).AddToQueue();
        target.TakeOtherDamage(damage);

    }

}
