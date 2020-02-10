using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongOfDestiny : CreatureEffect
{
    public int buffCooldown = 0;

    public SongOfDestiny(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if (creatureEffectCooldown <= 0)
        {

            ShowAbility();
            
            RemoveBuffsAll(target);

            if (target.Health > 10)
            {
                target.Health = 10;
                new UpdateHealthCommand(target.ID, target.Health).AddToQueue();
            }     

            base.UseEffect();
        }

    }    

}
