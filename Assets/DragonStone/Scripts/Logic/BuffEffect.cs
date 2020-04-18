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

    public int buffCD;

    public int buffCooldown
    {
        get {return buffCD;}
        set
        {
            buffCD = value;
            if(buffCD > 0)
            {
                new UpdateBuffCommand(this).AddToQueue();
            }
            if(buffCD <= 0)
                RemoveBuff();
        }
    }

    public bool isBuff;
    public bool isDebuff;

    [HideInInspector]
    public int defaultArmor = 10;

    public Sprite buffIcon;

    //DS
    //Added buffID for Logic and Visual link
    public int buffID;
    
    public BuffEffect(CreatureLogic source, CreatureLogic target, int buffCooldown)
    {
        this.source = source;
        this.target = target;
        this.buffCooldown = buffCooldown;

        this.buffID = IDFactory.GetUniqueID();
        Name = this.GetType().Name.ToString();
    }


    ~BuffEffect()
    {
        //Debug.Log("Buffeffect destroyed: " +this.GetType().Name);
    }

    public virtual void RegisterCooldown()
    {
        //TurnManager.Instance.e_ResetRound += ReduceCreatureEffectCooldown;
        target.timer.e_fullATB += ReduceCreatureEffectCooldown;
    }

    public virtual void UnregisterCooldown()
    {
       // TurnManager.Instance.e_ResetRound -= ReduceCreatureEffectCooldown;
       target.timer.e_fullATB -= ReduceCreatureEffectCooldown;
    }

    public virtual void CauseBuffEffect(){}

    public virtual void UndoBuffEffect(){}

    public void ReduceCreatureEffectCooldown()
    { 
        buffCooldown--;
        /*      
        if(buffCooldown > 0)
        {
            buffCooldown--;
            new UpdateBuffCommand(this).AddToQueue();
        }
        if(buffCooldown <= 0)
            RemoveBuff();
        */
    }

/* NO NEED IF PROP IS USED
    public virtual void UpdateCooldown()
    {
        new UpdateBuffCommand(this).AddToQueue();
    }
*/    

    public virtual void RemoveBuff()
    {
        UndoBuffEffect();
        UnregisterCooldown();
        target.buffEffects.Remove(this);       
        new DestroyBuffCommand(this, target.UniqueCreatureID).AddToQueue();

    }//RemoveBuff     

}
