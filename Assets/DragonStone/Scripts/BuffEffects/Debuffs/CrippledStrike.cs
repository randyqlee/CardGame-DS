using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrippledStrike : BuffEffect {

    int attackModifier = 8;
    
	
    public CrippledStrike(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
       buffIcon = Resources.Load<Sprite>("BuffIcons/CrippledStrike");
       isDebuff = true;
   }

    public override void CauseBuffEffect()
    {
       target.canDebuff = false;
       target.CriticalChance -=100f; 
         
    }

    public override void UndoBuffEffect()
    {
       target.canDebuff = true;
       target.CriticalChance +=100f;  
    }


    
    
}//DecreaseAttack Method
