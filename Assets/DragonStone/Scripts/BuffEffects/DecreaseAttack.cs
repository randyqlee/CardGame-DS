using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseAttack : BuffEffect {

    int attackModifier = 2;
	
    public DecreaseAttack(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {}

    public override void CauseEventEffect()
    {
        target.Attack -= attackModifier;    
    }

    public override void UndoEventEffect()
    {
        target.Attack += attackModifier;        
    }


    
    
}//DecreaseAttack Method
