using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class DragonsDance : CreatureEffect {

    public int buffCooldown = 1;
    int chance = 100;
    int fixedDamage = 5;
   
    

    public DragonsDance(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    { 
        fixedDamage = creature.Attack;
    }


   public override void RegisterEventEffect()
    {
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_AfterAttacking -= UseEffect;      
    }

    public override void CauseEventEffect()
    {
       
    }

    public override void UseEffect(CreatureLogic target)
    {     
        if(remainingCooldown <=0)
        {          
                       
            creature.SplashAttackDamage(target, creature.AttackDamage);

            base.UseEffect();
            

        }           

    }//UseEffect 

   

}
