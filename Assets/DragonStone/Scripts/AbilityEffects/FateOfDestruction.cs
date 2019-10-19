using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class FateOfDestruction : CreatureEffect
{
    public int buffCooldown = 2;
    bool chanceEffect = false;
    public FateOfDestruction(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
       creature.e_PreAttackEvent += IncreaseAttackCount;
       creature.e_SecondAttack += UseEffect;  
       //creature.e_PreAttackEvent += UseEffect;          
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_PreAttackEvent -= IncreaseAttackCount;
         creature.e_SecondAttack -= UseEffect;     
         //creature.e_PreAttackEvent -= UseEffect;      
    }

    void IncreaseAttackCount(CreatureLogic target)
    {
       
        if(CanUseAbility())
        {
            if(ChanceOK(creature.chance))
            {
                ShowAbility();
                AddBuff (target, "Bomb", buffCooldown);
                creature.AttacksLeftThisTurn++;
                chanceEffect = true;

            }
            else
            {
                base.UseEffect();
            }
            
        }                             
    }

    public override void UseEffect(CreatureLogic target)
    {
        if (CanUseAbility() && chanceEffect)       
        {
            //ShowAbility();
            if(!target.isDead)     
            {     
                creature.AttackCreature(target);
                //AddBuff (target, "Bomb", buffCooldown);               
                
            }

            base.UseEffect();  

        }

    }
}
