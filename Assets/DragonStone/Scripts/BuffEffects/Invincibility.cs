using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : BuffEffect {

    int attackModifier = 5;
    
	
    public Invincibility(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { isBuff = true;}

    public override void CauseBuffEffect()
    {
        
        target.DamageReduction = 0;
    }

    public override void UndoBuffEffect()
    {
        target.DamageReduction = 1;
    }


    
    
}//DecreaseAttack Method
