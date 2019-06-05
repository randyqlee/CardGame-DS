using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immunity : BuffEffect {

    int attackModifier = 5;
    
	
    public Immunity(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Immunity");
        isBuff = true;}

    public override void CauseBuffEffect()
    {
        
        target.canBeDebuffed = false; 
    }

    public override void UndoBuffEffect()
    {
        target.canBeDebuffed = true;        
    }


    
    
}//DecreaseAttack Method
