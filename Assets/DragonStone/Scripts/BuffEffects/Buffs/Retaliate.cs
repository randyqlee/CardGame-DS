using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retaliate : BuffEffect {

    int originalAttack;
    
	
    public Retaliate(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Retaliate");
        isBuff = true;}

    public override void CauseBuffEffect()
    {        
        target.targetAttackDamage = target.Attack;
        Debug.Log("Target's Attack Damage: " +target.targetAttackDamage);
    }

    public override void UndoBuffEffect()
    {
        target.targetAttackDamage = 0;   
    }

    
    
    
}//DecreaseAttack Method
