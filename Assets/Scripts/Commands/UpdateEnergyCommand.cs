using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEnergyCommand : Command {

    private int targetID;
    //private int amount;
    private float energy;
    //private int attackBefore;

    public UpdateEnergyCommand( int targetID, float energy)
    {
        this.targetID = targetID;
        //this.amount = amount;
        this.energy = energy;
        //this.attackBefore = attackBefore;
    }

    public override void StartCommandExecution()
    {
        //Debug.Log("In deal damage command!");

        GameObject target = IDHolder.GetGameObjectWithID(targetID);
       
            // target is a creature
            target.GetComponent<OneCreatureManager>().ChangeEnergy(energy);
        
        CommandExecutionComplete();
    }
}
