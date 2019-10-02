using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorsReturn : CreatureEffect
{
    public int healValue = 8;

    public WarriorsReturn(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       TurnManager.Instance.e_EndOfRound += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        TurnManager.Instance.e_EndOfRound -= UseEffect;          
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility() && ChanceOK(creature.chance))
        {   
            

            bool isAnAllyDead = false;
            foreach (CreatureLogic cl in owner.DeadAllyList())
            {
                isAnAllyDead = true;
                    break;
            }

            if (isAnAllyDead)
            {
                ShowAbility();

                int i = Random.Range(0,owner.DeadAllyList().Count);
                
                CreatureLogic ally = owner.deadAllies[i];

                ShowAbility();
                ally.Revive();
                ally.Health = healValue;
                new UpdateHealthCommand(ally.ID, ally.Health).AddToQueue();
            }

            base.UseEffect();
        }
    }
}