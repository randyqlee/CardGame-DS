using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuff : BuffEffect {


    public TestBuff(Player owner, CreatureLogic creature, int specialAmount): base(owner, creature, specialAmount)
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
            if (specialAmount > 0)
                specialAmount -= 1;
        }
    }		
	
}
