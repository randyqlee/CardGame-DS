using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffEffect
{

    public string Name; 
    
    [HideInInspector]
    public CreatureLogic source;
    [HideInInspector]
    public CreatureLogic target;

    public int buffCooldown;
    public bool isBuff;
    public bool isDebuff;

    public Sprite buffIcon;
    
    public BuffEffect(CreatureLogic source, CreatureLogic target, int buffCooldown)
    {
        this.source = source;
        this.target = target;
        this.buffCooldown = buffCooldown;

        Name = this.GetType().Name.ToString();
        

    }

    ~BuffEffect()
    {
        Debug.Log("Buffeffect destroyed: " +this.GetType().Name);
    }

    public virtual void RegisterCooldown()
    {
        target.e_CreatureOnTurnEnd += ReduceCreatureEffectCooldown;
    }

    public virtual void UnregisterCooldown()
    {
        target.e_CreatureOnTurnEnd -= ReduceCreatureEffectCooldown;
        
    }

    public virtual void CauseBuffEffect(){}

    public virtual void UndoBuffEffect(){}

    public void ReduceCreatureEffectCooldown()
    {       
        if(buffCooldown > 0)
            buffCooldown--;
        if(buffCooldown <= 0)
            RemoveBuff();
                        
    }

    public virtual void RemoveBuff()
    {
        Debug.Log("Remove Buff " +this.GetType().Name);
        UndoBuffEffect();
        UnregisterCooldown();        
        target.buffEffects.Remove(this);        

        
    }

}
