using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class DelayedPromise : CreatureEffect {

    public int buffCooldown = 1;

    public DelayedPromise(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
           
       creature.e_BeforeAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         
         creature.e_BeforeAttacking -= UseEffect;      
         
    }

    public override void CauseEventEffect()
    {
       //if(remainingCooldown <=0)
        //Debug.Log("Activate Effect: " +this.ToString());
    }

    public override void UseEffect(CreatureLogic target)
    {     
        if(remainingCooldown <=0)
        {     
          
           ShowAbility(target);

           ResetAllSkillCooldowns(target); 
           SilenceAllEnemies();
           //AddBuff(target,"Stun",buffCooldown);   

           base.UseEffect();                  
                
        }

        
    }

    

    public void ResetAllSkillCooldowns(CreatureLogic target)
    {
        foreach(CreatureEffect ce in target.creatureEffects)
        {
            ce.remainingCooldown = ce.creatureEffectCooldown;
        }
    }

    public void SilenceAllEnemies()
    {
        //Call Current Enemy list
        creature.owner.EnemyList();

        foreach(CreatureLogic cl in creature.owner.enemies)
        {
            AddBuff(cl, "Silence",buffCooldown);
        }

        //Clear enemy list
        creature.owner.enemies.Clear();
    }

}
