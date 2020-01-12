using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoaringWings : CreatureEffect
{
    public int buffCooldown = 2;

    public int stunCooldown = 1;

    bool critFlag = false;

    // Start is called before the first frame update
    public SoaringWings(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
       creature.e_PreAttackEvent += PreAttack;  
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= PreAttack; 
       creature.e_AfterAttacking -= UseEffect;           
    }

    public void PreAttack (CreatureLogic target)
    {
        if (CanUseAbility())
        {
            if(creature.isPrimaryForm)
            {
                ShowAbility();
                creature.CriticalChance += 1;
                critFlag = true;
            }
        }

    }

    public override void UseEffect(CreatureLogic target)
    {
        

        if(CanUseAbility())
        {
            ShowAbility();
            
            if(creature.isPrimaryForm)
            {
                creature.isPrimaryForm = false;
                new CreatureTransformCommand (creature.UniqueCreatureID, creature.isPrimaryForm).AddToQueue();
                AddBuff(creature, "Invincibility", buffCooldown);
                
                if(critFlag)
                {
                    creature.CriticalChance -= 1;
                    critFlag = false;
                }
            }

            else
            {
                AddBuff(target, "Stun", stunCooldown);

                creature.isPrimaryForm = true;
                new CreatureTransformCommand (creature.UniqueCreatureID, creature.isPrimaryForm).AddToQueue();
                AddBuff(creature, "Taunt", buffCooldown);
                
                
            }

            base.UseEffect();
            creature.ExtraTurn();
        }


    }


}
