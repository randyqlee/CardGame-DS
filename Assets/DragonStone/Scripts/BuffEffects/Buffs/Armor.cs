using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : BuffEffect
{
    public Armor(CreatureLogic source, CreatureLogic target, int buffCooldown, int specialValue = 2) : base (source, target, buffCooldown, specialValue)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Armor");
        isBuff = true;
    
    }

    public override void CauseBuffEffect()
    {
        
        int armorAfter = target.Armor + specialValue;
        new UpdateArmorCommand(target.ID, armorAfter).AddToQueue();

        target.Armor += specialValue;    
    }

    public override void UndoBuffEffect()
    {
        //comment these if effect will be permanent HP increase
        //int healthAfter = target.Health - specialValue;
        //new UpdateHealthCommand(target.ID, healthAfter).AddToQueue();    
        
        target.Armor -= specialValue;        
    }


}
