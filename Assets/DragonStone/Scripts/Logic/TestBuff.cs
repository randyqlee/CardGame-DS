using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuff : BuffEffect {


    public TestBuff(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}

   public override void RegisterEventEffect()
    {
        owner.EndTurnEvent += CauseEventEffect;
    }

    public override void UnRegisterEventEffect()
    {
        owner.EndTurnEvent -= CauseEventEffect;
    }

    public override void CauseEventEffect()
    {
        if (creature.isActive == true)
        {
            if (creatureEffectCooldown > 0)
                creatureEffectCooldown -= 1;
        }
    }		
	
}
