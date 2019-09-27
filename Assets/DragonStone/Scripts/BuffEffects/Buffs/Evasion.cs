using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evasion : BuffEffect
{
    // Start is called before the first frame update
    public Evasion(CreatureLogic source, CreatureLogic target, int buffCooldown, int specialValue = 50) : base (source, target, buffCooldown, specialValue)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Evasion");
        isBuff = true;    
    }

    public override void CauseBuffEffect()
    {        
        target.chanceTakeDamageFromAttack = target.chanceTakeDamageFromAttack * (specialValue/100);    
    }

    public override void UndoBuffEffect()
    {
        target.chanceTakeDamageFromAttack = target.chanceTakeDamageFromAttack / (specialValue/100);    ;    
    }
}