using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transcendance : CreatureEffect
{

    public int buffCooldown = 1;
    public Transcendance(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
        foreach(CreatureLogic cl in owner.EnemyList())
            cl.e_CreatureOnTurnEnd += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        foreach(CreatureLogic cl in owner.EnemyList())
            cl.e_CreatureOnTurnEnd -= UseEffect;               
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        { 
            if(ChanceOK(creature.chance) && creature.hasStun == false)
            {
                    ShowAbility();
                    AddBuff(creature, "IncreaseAttack", buffCooldown);
                    creature.ExtraTurn();
            }
                
            base.UseEffect();
        }
    }
}