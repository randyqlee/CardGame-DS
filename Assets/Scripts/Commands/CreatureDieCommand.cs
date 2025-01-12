﻿using UnityEngine;
using System.Collections;

public class CreatureDieCommand : Command 
{
    private Player p;
    private int DeadCreatureID;

    public CreatureDieCommand(int CreatureID, Player p)
    {
        this.p = p;
        this.DeadCreatureID = CreatureID;
    }

    public override void StartCommandExecution()
    {
        //ORIGINAL SCRIPT
        //p.PArea.tableVisual.RemoveCreatureWithID(DeadCreatureID);


Debug.Log("CreaturesOnTable " + p.table.CreaturesOnTable.Count);
        p.table.CreaturesOnTable.Remove(CreatureLogic.CreaturesCreatedThisGame[DeadCreatureID]);
Debug.Log("CreaturesOnTable " + p.table.CreaturesOnTable.Count);
        p.table.CreaturesOnGraveyard.Add(CreatureLogic.CreaturesCreatedThisGame[DeadCreatureID]);

        p.PArea.tableVisual.RemoveCreatureWithID(DeadCreatureID);

        p.CheckIfGameOver();

        Command.CommandExecutionComplete();
    }
}
