using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : BuffEffect
{
  
	
    public Armor(CreatureLogic source, CreatureLogic target, int buffCooldown, int specialValue) : base (source, target, buffCooldown, specialValue)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Armor");
        isBuff = true;
    
    }

    public override void CauseBuffEffect()
    {
        
        int healthAfter = target.Health + specialValue;
        new UpdateHealthCommand(target.ID, healthAfter).AddToQueue();

        target.Health += specialValue;    
    }

    public override void UndoBuffEffect()
    {
        int healthAfter = target.Health - specialValue;
        new UpdateHealthCommand(target.ID, healthAfter).AddToQueue();    
        
        target.Health -= specialValue;        
    }


}
