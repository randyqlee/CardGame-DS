using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : BuffEffect {
   
    int bombDamage = 10;
	
    public Bomb(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { 
        buffIcon = Resources.Load<Sprite>("BuffIcons/Bomb");
        isDebuff = true;}

    public override void CauseBuffEffect()
    {        
        target.e_CreatureOnTurnStart += bombEffect;
    }

    public override void UndoBuffEffect()
    {
        target.e_CreatureOnTurnStart -= bombEffect;
    }

    public void bombEffect()
    {        
        if(buffCooldown <= 0)
        {
            
        }

        
    }
    
    
}//DecreaseAttack Method
