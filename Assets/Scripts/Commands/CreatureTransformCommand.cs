using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureTransformCommand : Command
{
    private int CreatureID;

    private bool isPrimaryForm;

    public CreatureTransformCommand(int CreatureID, bool isPrimaryForm)
    {
        this.CreatureID = CreatureID;
        this.isPrimaryForm = isPrimaryForm;
    }
    // Start is called before the first frame update
    public override void StartCommandExecution()
    {

        GameObject g = IDHolder.GetGameObjectWithID(this.CreatureID);
		if (g!= null)
		{
            g.GetComponentInChildren<OverheadText>().FloatingText("Transform");
            if (this.isPrimaryForm)
			    g.GetComponent<OneCreatureManager>().CreatureGraphicImage.sprite = g.GetComponent<OneCreatureManager>().CreatureGraphicImage_Primary;
            else
                g.GetComponent<OneCreatureManager>().CreatureGraphicImage.sprite = g.GetComponent<OneCreatureManager>().CreatureGraphicImage_Secondary;
		}
        Command.CommandExecutionComplete();

    }
}
