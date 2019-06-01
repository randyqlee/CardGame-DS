using UnityEngine;
using System.Collections;

public class UpdateHealthCommand : Command {

    private int targetID;
    //private int amount;
    private int healthAfter;
    //private int attackBefore;

    public UpdateHealthCommand( int targetID, int healthAfter)
    {
        this.targetID = targetID;
        //this.amount = amount;
        this.healthAfter = healthAfter;
        //this.attackBefore = attackBefore;
    }

    public override void StartCommandExecution()
    {
        //Debug.Log("In deal damage command!");

        GameObject target = IDHolder.GetGameObjectWithID(targetID);
       
            // target is a creature
            target.GetComponent<OneCreatureManager>().ChangeHealth(healthAfter);
        
        CommandExecutionComplete();
    }
}
