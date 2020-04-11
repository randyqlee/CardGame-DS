using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlsPrayer : CreatureEffect
{
    public int buffCooldown = 2;

    public GirlsPrayer(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if (creatureEffectCooldown <= 0)
        {
            
            if(ChanceOK(creature.chance))
            {

                foreach (CreatureLogic cl in owner.AllyList())
                {
                    ShowAbility();
                    RemoveRandomDebuff(cl);
                    AddBuff(cl, "Recovery", buffCooldown);
                }
            }

            base.UseEffect();
        }

    }    

}
