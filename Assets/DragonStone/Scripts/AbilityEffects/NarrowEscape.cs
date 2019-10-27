using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrowEscape : CreatureEffect
{
    List<CreatureLogic> enemyList = new List<CreatureLogic>();
    CreatureLogic thisSource;
    CreatureLogic thisTarget;


    public NarrowEscape(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        enemyList = owner.otherPlayer.AllyList();
    }

   public override void RegisterEventEffect()
    {
        foreach(CreatureLogic cl in enemyList)
        {
            
            //Debug.Log("enemy hero: " +cl.Name);
            cl.e_PreAttackEvent2 += UseEffect;
             
        }


    }

    public override void UnRegisterEventEffect()
    {
        foreach(CreatureLogic cl in enemyList)
        {  
            
            cl.e_PreAttackEvent2 -= UseEffect;
             
        }
    }

    public void UseEffect(CreatureLogic source, CreatureLogic target)
    {
        if(CanUseAbility())
        {   
            
           if(ChanceOK(creature.chance) && target == creature)
            {
                ShowAbility();

                //method here
                Debug.Log("hero source: " +source.Name);
                Debug.Log("hero target: " +target.Name);
                Debug.Log("hero this: " +this.Name);

                int sourceAttackDamage = source.DealDamage(source.Attack);
                int targetsTotalLife = target.Health = target.Armor;

                if(sourceAttackDamage >= targetsTotalLife)                
                source.chanceTakeDamageFromAttack-=200;

            }
 

            base.UseEffect();
        }
    }

    

}