using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrantLife : CreatureEffect
{
    public int buffCooldown = 4;
    public int reviveHealth = 12;

    public GrantLife(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            
            if(Random.Range(0,100)<=creature.chance)
            {
                bool isAnAllyDead = false;
                foreach (CreatureLogic cl in owner.table.CreaturesOnTable)
                {
                    if (cl.isDead)
                    {
                        isAnAllyDead = true;
                        break;
                    }
                }

                if (isAnAllyDead)
                {

                    int i = Random.Range(0,owner.table.CreaturesOnTable.Count);
                    while (!owner.table.CreaturesOnTable[i].isDead)
                    {
                        i = Random.Range(0,owner.table.CreaturesOnTable.Count);
                    }
                    CreatureLogic ally = owner.table.CreaturesOnTable[i];

                    ShowAbility();
                    ally.Revive();
                    ally.Health = reviveHealth;
                    new UpdateHealthCommand(ally.ID, ally.Health).AddToQueue();
                    
                    AddBuff(creature, "Unlucky", buffCooldown);
                }

            }

            base.UseEffect();
        }

    }    
}
