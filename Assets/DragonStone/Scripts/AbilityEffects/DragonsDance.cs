using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class DragonsDance : CreatureEffect {

    public int buffCooldown = 1;
  
    

    public DragonsDance(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    { 

    }


   public override void RegisterEventEffect()
    {
       creature.e_AfterAttacking += UseEffect;      
  
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_AfterAttacking -= UseEffect;      
        
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {   
            if(ChanceOK(creature.chance))
            {
                ShowAbility();
                foreach(CreatureLogic cl in owner.EnemyList())
                {
                    DealDamageEffect(cl, creature.AttackDamage);
                    AddBuff(cl, "Silence", buffCooldown);
                    cl.RemoveRandomBuff();
                }
  
            }
            
            base.UseEffect();
        }
    }

}
