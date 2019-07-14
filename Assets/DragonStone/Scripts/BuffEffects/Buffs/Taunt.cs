using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : BuffEffect {   
	
    public Taunt(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { 
        buffIcon = Resources.Load<Sprite>("BuffIcons/Taunt");
        isBuff = true;}

    
    List<bool> tauntStatus = new List<bool>();
    
    public override void CauseBuffEffect()
    {       
        target.hasTaunt = true;

        foreach(CreatureLogic cl in target.owner.allies)
        {
            if(!cl.hasTaunt)
            {
                cl.canBeAttacked = false;
            }
        }

        //target.canBeAttacked = true;
        
    }

    public override void UndoBuffEffect()
    {
        target.hasTaunt = false;

        foreach(CreatureLogic cl in target.owner.allies)
        {
           tauntStatus.Add(cl.hasTaunt);
        }

        //if there are no more taunts
        if(!tauntStatus.Contains(true))
        {
             foreach(CreatureLogic cl in target.owner.allies)
            {                
                cl.canBeAttacked = true;                
            }
        }

    }


    
    
}//DecreaseAttack Method
