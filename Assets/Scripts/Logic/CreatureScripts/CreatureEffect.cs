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

    [HideInInspector]
    public CreatureLogic creature;

    //DS
    public int creatureEffectCooldown;
    public int remainingCooldown;
   

   
    public CreatureEffect(Player owner, CreatureLogic creature, int creatureEffectCooldown)
    {
        this.creature = creature;
        this.owner = owner;
        this.creatureEffectCooldown = creatureEffectCooldown;
        
        //initialize remaining cooldown
        remainingCooldown = creatureEffectCooldown;
    }

    // METHODS FOR SPECIAL FX THAT LISTEN TO EVENTS
    public virtual void RegisterEventEffect()
    {
        
    }

    public virtual void UnRegisterEventEffect(){}

    public virtual void CauseEventEffect(){}

    // BATTLECRY
    public virtual void WhenACreatureIsPlayed(){}

    // DEATHRATTLE
    public virtual void WhenACreatureDies(){}

    public virtual void RegisterCooldown()
    {
        creature.e_CreatureOnTurnStart += ReduceCreatureEffectCooldown;
    }

    public virtual void UnregisterCooldown()
    {
        creature.e_CreatureOnTurnStart -= ReduceCreatureEffectCooldown;
    }

    public void ReduceCreatureEffectCooldown()
    {       
        if(remainingCooldown > 0)
            remainingCooldown--;
        else
            remainingCooldown = creatureEffectCooldown;            
    }

    //DS
    //Use ability overrides
    public virtual void UseEffect(CreatureLogic target)
    {

    }

    public virtual void UseEffect(int uniqueCreatureID)
    {

    }

    public virtual void AddBuff(CreatureLogic target, string buffName, int buffCooldown)
    {
        //the BuffEffect will be instantiated here
        BuffEffect buffEffect = System.Activator.CreateInstance(System.Type.GetType(buffName), new System.Object[]{creature, target, buffCooldown}) as BuffEffect;
        

        //if buff, can only affect allies
        if(buffEffect.isBuff && creature.canBuff && target.canBeBuffed)
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
        
        
        

        //the logic of adding buff to the CReatureLogic will be in a method at CreatureLogic
        //target.AddBuff(buffEffect);
       
    }

    public virtual void RemoveBuff(CreatureLogic target, BuffEffect buff)
    {

    }     

}
