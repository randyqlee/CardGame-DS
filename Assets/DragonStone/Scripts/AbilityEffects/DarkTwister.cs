using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkTwister : CreatureEffect
{
    public int value = 10;


    public DarkTwister(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        
            if(Random.Range(0,100)<=creature.chance)
            {
                ShowAbility();
                creature.SplashAttackDamage(target, value); 
                //DealDamageEffect(target, damage);               
                foreach(CreatureLogic cl in owner.AllyList())
                {
                    cl.Heal(value);
                }
            }              
                
            base.UseEffect();
        }

    }

}
