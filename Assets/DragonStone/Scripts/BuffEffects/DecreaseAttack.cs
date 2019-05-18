using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseAttack : BuffEffect {

    int attackModifier = 2;
    
	
    public DecreaseAttack(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { isDebuff = true;}

    public override void CauseBuffEffect()
    {
        
        int attackAfter = target.Attack - attackModifier;
        new UpdateAttackCommand(target.ID, attackAfter).AddToQueue();

        target.Attack -= attackModifier;    
    }

    public override void UndoBuffEffect()
    {
        int attackAfter = target.Attack + attackModifier;
        new UpdateAttackCommand(target.ID, attackAfter).AddToQueue();    
        
        target.Attack += attackModifier;        
    }


    
    
}//DecreaseAttack Method
