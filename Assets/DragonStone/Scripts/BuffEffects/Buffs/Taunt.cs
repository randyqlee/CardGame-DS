using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : BuffEffect {   
	
    public Taunt(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { 
        buffIcon = Resources.Load<Sprite>("BuffIcons/Taunt");
        isBuff = true;}

    
    List<bool> tauntStatus = new List<bool>();

    bool tauntFound = false;
    
    public override void CauseBuffEffect()
    {       
        target.HasTaunt = true;
        target.canBeAttacked = true;

        foreach(CreatureLogic cl in target.owner.AllyList())
        {
            if(!cl.HasTaunt && cl != target)
            {
                cl.canBeAttacked = false;
            }
        }

        //target.canBeAttacked = true;
        
    }

    public override void UndoBuffEffect()
    {
        target.HasTaunt = false;
        tauntFound = false;
/*
        foreach(CreatureLogic cl in target.owner.allies)
        {
           tauntStatus.Add(cl.HasTaunt);
        }

        //if there are no more taunts
        if(!tauntStatus.Contains(true))
        {
             foreach(CreatureLogic cl in target.owner.allies)
            {                
                cl.canBeAttacked = true;                
            }
        }
*/

        foreach(CreatureLogic cl in target.owner.AllyList())
        {
            if(cl.HasTaunt && cl != target)
            {
                target.canBeAttacked = false;
                tauntFound = true;
                break;
            }
        }

        if (!tauntFound)
        {
            foreach(CreatureLogic cl in target.owner.AllyList())
            {                
                cl.canBeAttacked = true;                
            }
        }

    }


    
    
}//DecreaseAttack Method
