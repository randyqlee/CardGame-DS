using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class DragonsDance : CreatureEffect {

    public int buffCooldown = 1;
    int chance = 75;
    

    public DragonsDance(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


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
            
            foreach(CreatureLogic cl in owner.enemies)
            {            
               
              if(Random.Range(0,100) <= chance)
              {
                  AddBuff(target,"Stun",1);
              }
              
              if(cl != target)
              {
                  DealDamage(cl);
              }
                         
            }

            base.UseEffect();
            

        }           

    }//UseEffect

    void DealDamage(CreatureLogic target)
    {
        new DelayCommand(0.5f).AddToQueue();        

        new DealDamageCommand(target.ID, creature.AttackDamage, healthAfter: target.TakeDamageVisual(target.DealDamage(creature.AttackDamage))).AddToQueue();

        target.TakeDamage(creature.DealDamage(creature.AttackDamage));    
        
    }

   

}
