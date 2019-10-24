using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForbiddenGaldr : CreatureEffect
{
 
    public ForbiddenGaldr(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       creature.e_PreAttackEvent += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= UseEffect;          
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {
            int armor = creature.Armor;
            if (armor > 0)
            {
                ShowAbility();
                foreach(CreatureLogic cl in owner.AllyList())
                {
                    cl.Heal(armor);
                }
                
                foreach(CreatureLogic cl in owner.EnemyList())
                {
                    DealDamageEffect(cl, armor);
                }               

                base.UseEffect();
            }
            
            else
                hasUsedEffect = false;
        }
    }

}
