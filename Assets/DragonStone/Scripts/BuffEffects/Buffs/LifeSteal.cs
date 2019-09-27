using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSteal : BuffEffect
{
    // Start is called before the first frame update
    public LifeSteal(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/LifeSteal");
        isBuff = true;    
    }

    public override void CauseBuffEffect()
    {        
        target.e_IsDamagedByAttack += StealLife;    
    }

    public override void UndoBuffEffect()
    {
        target.e_IsDamagedByAttack -= StealLife;    
    }

    public void StealLife(CreatureLogic source, CreatureLogic target, int damage)
    {

        source.Heal(damage);

    }
}
