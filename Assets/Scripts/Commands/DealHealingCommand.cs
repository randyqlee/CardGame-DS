﻿using UnityEngine;
using System.Collections;

public class DealHealingCommand : Command {

    private int targetID;
    private int amount;
    private int healthAfter;

    public DealHealingCommand( int targetID, int amount, int healthAfter)
    {
        this.targetID = targetID;
        this.amount = amount;
        this.healthAfter = healthAfter;
    }

    public override void StartCommandExecution()
    {
        //Debug.Log("In deal damage command!");

        GameObject target = IDHolder.GetGameObjectWithID(targetID);
        if (targetID == GlobalSettings.Instance.LowPlayer.PlayerID || targetID == GlobalSettings.Instance.TopPlayer.PlayerID)
        {
            // target is a hero
            target.GetComponent<PlayerPortraitVisual>().TakeDamage(amount,healthAfter);
        }
        else
        {
            // target is a creature
            target.GetComponent<OneCreatureManager>().TakeHealing(amount, healthAfter);
        }
        CommandExecutionComplete();
    }
}
