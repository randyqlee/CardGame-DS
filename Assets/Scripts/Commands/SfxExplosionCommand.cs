using UnityEngine;
using System.Collections;

public class SfxExplosionCommand : Command {

    private int targetID;
    //private int amount;
    private int healthAfter;
    //private int attackBefore;

    public SfxExplosionCommand(int targetID)
    {
        this.targetID = targetID;
        //this.amount = amount;
        
    }

    public override void StartCommandExecution()
    {
        
        GameObject target = IDHolder.GetGameObjectWithID(targetID);
       
            // target is a creature           
            target.GetComponent<OneCreatureManager>().Explode();
        
        CommandExecutionComplete();
    }
}
