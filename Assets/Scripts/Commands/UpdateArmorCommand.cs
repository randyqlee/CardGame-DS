using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateArmorCommand : Command {

    private int targetID;
    //private int amount;
    private int armorAfter;
    //private int attackBefore;
    private CreatureLogic targetHero;

    public UpdateArmorCommand( int targetID, int armorAfter)
    {
        this.targetID = targetID;
        //this.amount = amount;
        this.armorAfter = armorAfter;
        //this.attackBefore = attackBefore;
        targetHero = CreatureLogic.CreaturesCreatedThisGame[targetID];
        
    }

    public override void StartCommandExecution()
    {
        //Debug.Log("In deal damage command!");

        GameObject target = IDHolder.GetGameObjectWithID(targetID);
       
            // target is a creature
            target.GetComponent<OneCreatureManager>().ChangeArmor(armorAfter);

            //Test Script for armor buff destroy
            //Debug.Log("Command Hero Name: " +targetHero.Name);
            // if(armorAfter <=0)
            // {
            //     for(int i = targetHero.buffEffects.Count-1; i>=0; i--)
            //     {
            //         if(targetHero.buffEffects[i].Name == "Armor")
            //         {
            //              Debug.Log("Command Buff: " +targetHero.buffEffects[i].Name);
            //              targetHero.RemoveBuff(targetHero.buffEffects[i]);
            //         }
                    
            //     }
            // }
            
        
        CommandExecutionComplete();
    }
}