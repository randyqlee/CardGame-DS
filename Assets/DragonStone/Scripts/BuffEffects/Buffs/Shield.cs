using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BuffEffect {

   
    
	
    public Shield(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Shield");
        isBuff = true;}

    public override void CauseBuffEffect()
    {
        
        target.DamageReduction = 0;
        target.e_IsComputeDamage += ShieldEffect;
    }

    public override void UndoBuffEffect()
    {
        
    }

    public void ShieldEffect()
    {
        
        target.DamageReduction = 1;
        //target.buffEffects.Remove(this);
        //buffCooldown = 0;
        target.e_IsComputeDamage -= ShieldEffect;
        RemoveBuff();

    }

    
    
}//DecreaseAttack Method
