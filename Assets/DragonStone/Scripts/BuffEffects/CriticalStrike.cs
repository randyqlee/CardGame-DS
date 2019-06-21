using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalStrike : BuffEffect {

    int attackModifier = 5;
    
	
    public CriticalStrike(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/CriticalStrike");
        isBuff = true;}

    public override void CauseBuffEffect()
    {
       
       target.CriticalChance = 2f;
    }

    public override void UndoBuffEffect()
    {
          
        target.CriticalChance = 0f;        
    }


    
    
}//DecreaseAttack Method
