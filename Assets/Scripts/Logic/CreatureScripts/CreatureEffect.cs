using UnityEngine;
using System.Collections;

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

    [HideInInspector]
    public CreatureLogic creature;

   

    //DS
    public int creatureEffectCooldown;
    public int remainingCooldown;
    public bool hasUsedEffect = false;
    
    

   
    public CreatureEffect(Player owner, CreatureLogic creature, int creatureEffectCooldown)
    {
        this.creature = creature;
        this.owner = owner;
        this.creatureEffectCooldown = creatureEffectCooldown;
        
        //initialize remaining cooldown
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

    public virtual void UnRegisterEventEffect(){}

    public virtual void CauseEventEffect(){}

    // BATTLECRY
    public virtual void WhenACreatureIsPlayed(){
        
    }

    // DEATHRATTLE
    public virtual void WhenACreatureDies(){}

    public virtual void RegisterCooldown()
    {
        creature.e_CreatureOnTurnStart += ReduceCreatureEffectCooldown;
        //TurnManager.e_ResetRound += ReduceCreatureEffectCooldown;
        creature.e_CreatureOnTurnEnd += ResetCreatureEffectCooldown;
    }

    public virtual void UnregisterCooldown()
    {
        creature.e_CreatureOnTurnStart -= ReduceCreatureEffectCooldown;
        //TurnManager.e_ResetRound -= ReduceCreatureEffectCooldown;
        creature.e_CreatureOnTurnEnd -= ResetCreatureEffectCooldown;
    }

    public void ReduceCreatureEffectCooldown()
    {       
        if(remainingCooldown > 0){
            remainingCooldown--;
        }
        

        new UpdateCooldownCommand (this.abilityCard, remainingCooldown, creatureEffectCooldown).AddToQueue();
            
    }

    public void ResetCreatureEffectCooldown()
    {       
       if(remainingCooldown<=0 && creature.canUseAbility)
        {
            if(hasUsedEffect)
            remainingCooldown = creatureEffectCooldown;            
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
        //the BuffEffect will be instantiated here
        BuffEffect buffEffect = System.Activator.CreateInstance(System.Type.GetType(buffName), new System.Object[]{creature, target, buffCooldown}) as BuffEffect;
        

        //if buff, can only affect allies
        if(buffEffect.isBuff && creature.canBuff && target.canBeBuffed)
        //if(buffEffect.isBuff)
        {
            //check if same team
            if(target.owner == creature.owner)
            {
                target.AddBuff(buffEffect);        
            }
        }

        //if debuff, can only affect enemies
        if(buffEffect.isDebuff && creature.canDebuff && target.canBeDebuffed)
        {
            //check if same team
            if(target.owner != creature.owner)
            {
                target.AddBuff(buffEffect);        
            }
        }
        
        // Debug.Log("Is Buff? " +buffEffect.isBuff);
        // Debug.Log("can Buff? " +creature.canBuff);
        // Debug.Log("target can be Buffed? " +target.canBeBuffed);

        // Debug.Log("Is Debuff? " +buffEffect.isDebuff);
        // Debug.Log("can Debuff? " +creature.canDebuff);
        // Debug.Log("target can be Debuffed? " +target.canBeDebuffed);
        

        //the logic of adding buff to the CReatureLogic will be in a method at CreatureLogic
        //target.AddBuff(buffEffect);
       
    }

    public virtual void RemoveBuff(CreatureLogic target, BuffEffect buff)
    {

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

}
