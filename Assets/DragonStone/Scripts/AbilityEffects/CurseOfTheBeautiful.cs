using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class CurseOfTheBeautiful : CreatureEffect {

	public CurseOfTheBeautiful(Player owner, CreatureLogic creature, int specialAmount): base(owner, creature, specialAmount)
    {}

   public override void RegisterEventEffect()
    {
         //creature.e_CreatureOnTurnStart += CauseEventEffect;
         creature.e_PreAttackEvent += UseEffect;
    }

    public override void UnRegisterEventEffect()
    {
        //creature.e_CreatureOnTurnStart -= CauseEventEffect;
        creature.e_PreAttackEvent -= UseEffect;
    }

    public override void CauseEventEffect()
    {
    //    if(remainingCooldown <=0)
    //     Debug.Log("Activate Effect: " +this.ToString());
    }

    
    
}
