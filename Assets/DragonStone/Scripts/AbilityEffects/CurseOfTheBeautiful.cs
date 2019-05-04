using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseOfTheBeautiful : CreatureEffect {

	public CurseOfTheBeautiful(Player owner, CreatureLogic creature, int specialAmount): base(owner, creature, specialAmount)
    {}

   public override void RegisterEventEffect()
    {
        creature.e_CreatureOnTurnStart += CauseEventEffect;
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_CreatureOnTurnStart -= CauseEventEffect;
    }

    public override void CauseEventEffect()
    {
       if(remainingCooldown <=0)
        Debug.Log("Activate Effect: " +this.ToString());
    }

    
    
}
