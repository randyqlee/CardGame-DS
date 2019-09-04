using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureColorCommand : Command {

	CreatureLogic cl;
	bool hasAttacked;
	public CreatureColorCommand(CreatureLogic cl, bool hasAttacked)
	{
		this.cl = cl;
		this.hasAttacked = hasAttacked;

	}

	    public override void StartCommandExecution()
    {
		GameObject g = IDHolder.GetGameObjectWithID(cl.UniqueCreatureID);
		if (g!= null)
		{
			g.GetComponent<OneCreatureManager>().HasAttacked = hasAttacked;
		}

		Command.CommandExecutionComplete();
	}


}
