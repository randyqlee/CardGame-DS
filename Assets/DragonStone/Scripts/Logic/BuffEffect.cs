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

    public int specialValue;

    public Sprite buffIcon;

    //DS
    //Added buffID for Logic and Visual link
    public int buffID;
    
    public BuffEffect(CreatureLogic source, CreatureLogic target, int buffCooldown)
    {
        this.source = source;
        this.target = target;
        this.buffCooldown = buffCooldown;

    //DS
    //Added buffID for Logic and Visual link
        this.buffID = IDFactory.GetUniqueID();

        Name = this.GetType().Name.ToString();
        

    }

    public BuffEffect(CreatureLogic source, CreatureLogic target, int buffCooldown, int specialValue)
    {
        this.source = source;
        this.target = target;
        this.buffCooldown = buffCooldown;
        this.specialValue = specialValue;

    //DS
    //Added buffID for Logic and Visual link
        this.buffID = IDFactory.GetUniqueID();

        Name = this.GetType().Name.ToString();
        

    }

    ~BuffEffect()
    {
        //Debug.Log("Buffeffect destroyed: " +this.GetType().Name);
    }

    public virtual void RegisterCooldown()
    {
        TurnManager.Instance.e_ResetRound += ReduceCreatureEffectCooldown;
        //target.e_CreatureOnTurnEnd += ReduceCreatureEffectCooldown;
    }

    public virtual void UnregisterCooldown()
    {
        TurnManager.Instance.e_ResetRound -= ReduceCreatureEffectCooldown;
        //target.e_CreatureOnTurnEnd -= ReduceCreatureEffectCooldown;
        
    }

    public virtual void CauseBuffEffect(){}

    public virtual void UndoBuffEffect(){}

    public void ReduceCreatureEffectCooldown()
    {       
        if(buffCooldown > 0)
        {
            buffCooldown--;
            //insert UpdateBuffCommand to update the cooldown text
            new UpdateBuffCommand(this).AddToQueue();

        

        }
        if(buffCooldown <= 0)

            RemoveBuff();
            
                        
    }

    public virtual void UpdateCooldown()
    {
        new UpdateBuffCommand(this).AddToQueue();
    }
    

    public virtual void AddBuff(CreatureLogic target, string buffName, int buffCooldown)
    {
        //the BuffEffect will be instantiated here
        BuffEffect buffEffect = System.Activator.CreateInstance(System.Type.GetType(buffName), new System.Object[]{source, target, buffCooldown}) as BuffEffect;
        

        //if buff, can only affect allies
        if(buffEffect.isBuff && source.canBuff && target.canBeBuffed)
        //if(buffEffect.isBuff)
        {
            //check if same team
            if(target.owner == source.owner)
            {
                target.AddBuff(buffEffect);        
            }
        }

        //if debuff, can only affect enemies
        if(buffEffect.isDebuff && source.canDebuff && target.canBeDebuffed)
        {
            //check if same team
            if(target.owner != source.owner)
            {
                target.AddBuff(buffEffect);        
            }
        }
        
        
    }// Addbuff

    public virtual void RemoveBuff()
    {
        //Debug.Log("Remove Buff " +this.GetType().Name);
        UndoBuffEffect();
        UnregisterCooldown();        
        target.buffEffects.Remove(this);       
         
        new DestroyBuffCommand(this, target.UniqueCreatureID).AddToQueue();

        
    }//RemoveBuff     

}
