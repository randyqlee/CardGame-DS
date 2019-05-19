using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiBuff : BuffEffect 
{

    int attackModifier = 8;
    
	
    public AntiBuff(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { isDebuff = true;}

    public override void CauseBuffEffect()
    {
      target.canBeBuffed = false;
       
         
    }

    public override void UndoBuffEffect()
    {
       target.canBeBuffed = true; 
    }


    
    
}//DecreaseAttack Method
