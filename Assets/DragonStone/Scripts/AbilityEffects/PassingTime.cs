using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PassingTime : CreatureEffect {

    public int buffCooldown = 0;
    int chance = 100;

    public PassingTime(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_CreatureOnTurnStart += IncreaseAttackCount;
       creature.e_SecondAttack += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_CreatureOnTurnStart -= IncreaseAttackCount;
         creature.e_SecondAttack -= UseEffect;      
    }

    public override void CauseEventEffect()
    {
       
    }

    public override void UseEffect(CreatureLogic target)
    {        
        
        if(remainingCooldown<=0)
        {
            base.UseEffect();
            SecondAttack(target);          

        }        
        
    }

    void IncreaseAttackCount()
    {
       
        if(remainingCooldown<=0)
        {
            creature.AttacksLeftThisTurn++;     
        }                             
    }

    void SecondAttack(CreatureLogic target)
    {               
        creature.AttackCreature(target);
    }

    void CriticalStrike()
    {
        AddBuff(creature, "CriticalStrike", 1);
    }
}
