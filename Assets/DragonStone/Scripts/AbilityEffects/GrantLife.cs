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
            
            if(ChanceOK(creature.chance))
            {
                bool isAnAllyDead = false;
                foreach (CreatureLogic cl in owner.DeadAllyList())
                {
                    isAnAllyDead = true;
                        break;
                }

                if (isAnAllyDead)
                {

                    int i = Random.Range(0,owner.DeadAllyList().Count);
                    
                    CreatureLogic ally = owner.DeadAllyList()[i];

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
