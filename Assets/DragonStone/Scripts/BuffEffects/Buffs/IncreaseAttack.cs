using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAttack : BuffEffect {

    int attackModifier = 5;
    
	
    public IncreaseAttack(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/IncreaseAttack");
        isBuff = true;}

    public override void CauseBuffEffect()
    {
        
        int attackAfter = target.Attack + attackModifier;
        new UpdateAttackCommand(target.ID, attackAfter).AddToQueue();

        target.Attack += attackModifier;    
    }

    public override void UndoBuffEffect()
    {
        int attackAfter = target.Attack - attackModifier;
        new UpdateAttackCommand(target.ID, attackAfter).AddToQueue();    
        
        target.Attack -= attackModifier;        
    }


    
    
}//DecreaseAttack Method
