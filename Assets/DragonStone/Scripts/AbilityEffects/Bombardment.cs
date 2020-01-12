using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombardment : CreatureEffect
{
    int buffCooldown = 1;
    int enemyCount = 2;

    public Bombardment(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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

            List<CreatureLogic> validTargets = new List<CreatureLogic>();

            foreach (CreatureLogic cl in owner.EnemyList())
            {
                if (cl.canBeDebuffed)
                {
                    validTargets.Add(cl);
                }
            }

            if (validTargets.Count > 0)
            {
                for (int i=0; i<enemyCount; i++)
                {
                    if (validTargets.Count > 0)
                    {
                        int j = Random.Range(0,validTargets.Count);
                        CreatureLogic enemy = validTargets[j];

                        if(Random.Range(0,100)<=creature.chance)
                        {
                            ShowAbility();
                            AddBuff(enemy, "CrippledStrike", buffCooldown);
                        }

                        validTargets.RemoveAt(j);
                    }
                }
            }
            
            base.UseEffect();
        }

    }    
}
