using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnleashedFury : CreatureEffect
{
    public int buffCooldown = 3;
    // Start is called before the first frame update
    public UnleashedFury(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
       //creature.e_CreatureOnTurnStart += UseEffect;      
       creature.e_PreAttackEvent += IncreaseAttackCount;  
       creature.e_SecondAttack += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_PreAttackEvent -= IncreaseAttackCount;  
         creature.e_SecondAttack -= UseEffect;          
    }

    void IncreaseAttackCount(CreatureLogic target)
    {
       
        if(CanUseAbility())
        {
            ShowAbility();
            creature.AttacksLeftThisTurn++;
            AddBuff(creature, "IncreasedAttack", buffCooldown);
            AddBuff(creature, "Immunity", buffCooldown);

        }                             
    }
    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {
            ShowAbility();
            if (!target.isDead)
            {
                creature.AttackCreature(target);
            }
            base.UseEffect();
        }

    }

}
