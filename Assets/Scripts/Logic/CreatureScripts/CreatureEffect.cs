using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class CreatureEffect
{
    protected Player owner;
    protected CreatureLogic creature;
    //protected int creatureEffectCooldown;

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
    

}
