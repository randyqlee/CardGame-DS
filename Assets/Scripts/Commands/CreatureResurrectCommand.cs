using UnityEngine;
using System.Collections;

public class CreatureResurrectCommand : Command 
{
    private Player p;
    private int ResurrectCreatureID;

    public CreatureResurrectCommand(int CreatureID, Player p)
    {
        this.p = p;
        this.ResurrectCreatureID = CreatureID;
    }

    public override void StartCommandExecution()
    {
        //ORIGINAL SCRIPT
        //p.PArea.tableVisual.RemoveCreatureWithID(DeadCreatureID);

        p.PArea.tableVisual.ResurrectCreatureWithID(ResurrectCreatureID);
    }
}
