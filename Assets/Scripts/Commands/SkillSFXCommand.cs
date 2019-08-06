using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSFXCommand : Command {

	//private CreatureEffect ce;
    private int targetID;
    //private int amount;

    public enum SFXStates
    {
        Attack,
        TakeDamage,
        UseSkill

    };

    private SFXStates state;

    //public SkillSFXCommand(CreatureEffect ce, int targetID, SFXStates state)
    public SkillSFXCommand(int targetID, SFXStates state)
    {
        
		this.targetID = targetID;
        this.state = state;
          
    }

    public override void StartCommandExecution()
    {
        
        GameObject target = IDHolder.GetGameObjectWithID(targetID);
       
       switch (state)
       {
           case SFXStates.Attack:

           //change this to creature effect SFX
            SpecialEffect.CreateAttackEffect(target.transform.position, 1);
            //CommandExecutionComplete();

           break;


           case SFXStates.TakeDamage:

           //change this to creature effect SFX
            SpecialEffect.CreateDamageEffect(target.transform.position, 1);
            //CommandExecutionComplete();

           break;


           case SFXStates.UseSkill:

           //change this to creature effect SFX
            SpecialEffect.CreateSkillEffect(target.transform.position, 1);
            //CommandExecutionComplete();

           break;
       }

		
    }

}
