using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffEffect {

    public CreatureLogic source;
    public CreatureLogic target;
    public int buffCooldown;
    public BuffEffect(CreatureLogic source, CreatureLogic target, int buffCooldown)
    {
        this.source = source;
        this.target = target;
        this.buffCooldown = buffCooldown;

    }

    ~BuffEffect()
    {
        Debug.Log("Buffeffect destroyed");
    }

    public virtual void RegisterCooldown()
    {
        target.e_CreatureOnTurnStart += ReduceCreatureEffectCooldown;
    }

    public virtual void UnregisterCooldown()
    {
        target.e_CreatureOnTurnStart -= ReduceCreatureEffectCooldown;
    }

    public void ReduceCreatureEffectCooldown()
    {       
        if(buffCooldown > 0)
            buffCooldown--;
        if(buffCooldown == 0)
            RemoveBuff();
                        
    }

    public void RemoveBuff()
    {
        UnregisterCooldown();
        target.buffEffects.Remove(this);

    }

}
