using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlucky : BuffEffect
{
  
  int specialValue = 100;
	
    public Unlucky(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Unlucky");
        isDebuff= true;
    
    }

    public override void CauseBuffEffect()
    {
        target.chance -= specialValue;    
    }

    public override void UndoBuffEffect()
    {
        target.chance += specialValue;        
    }
}