using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSFXCommand : Command {

	private CreatureEffect ce;
    private int targetID;
    //private int amount;

    public SkillSFXCommand(CreatureEffect ce, int targetID)
    {
        this.ce = ce;
		this.targetID = targetID;
  
        
    }

    public override void StartCommandExecution()
    {
        
        GameObject target = IDHolder.GetGameObjectWithID(targetID);
       
		//change this to creature effect SFX
        SpecialEffect.CreateDamageEffect(target.transform.position, 1);
        //CommandExecutionComplete();
    }

}
