using UnityEngine;
using System.Collections;

public class SplashDamageCommand : Command {

    private int targetID;
    private int splashDamage;
    private int healthAfter;

    public SplashDamageCommand( int targetID, int splashDamage)
    {
        this.targetID = targetID;
        this.splashDamage = splashDamage;
        
    }

    public override void StartCommandExecution()
    {
        
        GameObject targetEnemy = IDHolder.GetGameObjectWithID(targetID);    

        if(splashDamage>0) 
	    DamageEffect.CreateDamageEffect(targetEnemy.transform.position, splashDamage);        

        CommandExecutionComplete();
    }
}
