using UnityEngine;
using System.Collections;

public class UpdateAttackCommand : Command {

    private int targetID;
    //private int amount;
    private int attackAfter;
    //private int attackBefore;

    public UpdateAttackCommand( int targetID, int attackAfter)
    {
        this.targetID = targetID;
        //this.amount = amount;
        this.attackAfter = attackAfter;
        //this.attackBefore = attackBefore;
    }

    public override void StartCommandExecution()
    {
        //Debug.Log("In deal damage command!");

        GameObject target = IDHolder.GetGameObjectWithID(targetID);
       
            // target is a creature
            target.GetComponent<OneCreatureManager>().ChangeAttack(attackAfter);
        
        CommandExecutionComplete();
    }
}
