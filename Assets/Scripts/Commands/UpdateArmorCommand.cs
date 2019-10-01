using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateArmorCommand : Command {

    private int targetID;
    //private int amount;
    private int armorAfter;
    //private int attackBefore;

    public UpdateArmorCommand( int targetID, int armorAfter)
    {
        this.targetID = targetID;
        //this.amount = amount;
        this.armorAfter = armorAfter;
        //this.attackBefore = attackBefore;
    }

    public override void StartCommandExecution()
    {
        //Debug.Log("In deal damage command!");

        GameObject target = IDHolder.GetGameObjectWithID(targetID);
       
            // target is a creature
            target.GetComponent<OneCreatureManager>().ChangeArmor(armorAfter);
        
        CommandExecutionComplete();
    }
}