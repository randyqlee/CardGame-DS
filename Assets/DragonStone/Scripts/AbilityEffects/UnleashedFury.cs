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
       creature.e_PreAttackEvent += UseEffect;  
       creature.e_AfterAttacking += SecondAttack;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_PreAttackEvent -= UseEffect;  
         creature.e_AfterAttacking -= SecondAttack;          
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(creatureEffectCooldown <= 0)
        {
            ShowAbility();
            AddBuff(creature, "IncreasedAttack", buffCooldown);
            AddBuff(creature, "Immunity", buffCooldown);
            creature.SplashAttackDamage(target, creature.AttackDamage);
        }

        
    }

    public void SecondAttack(CreatureLogic target)
    {
        if(creatureEffectCooldown <= 0)
        {
            if (!target.isDead)
            {
                creature.AttackCreature(target);
            }
            base.UseEffect();
        }
    }
}
