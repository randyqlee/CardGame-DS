using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongOfTheNightWind : CreatureEffect
{
    public int buffCooldown = 2;

    public SongOfTheNightWind(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if(creatureEffectCooldown <= 0)
        {
            ShowAbility();
            foreach(CreatureLogic cl in owner.AllyList())
            {
                AddBuff(cl, "CriticalStrike", buffCooldown);
                AddBuff(cl, "Retaliate", buffCooldown);
            }
            base.UseEffect();
        }
    }
}
