using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerResetCommand : Command
{
    // Start is called before the first frame update
    CreatureLogic cl;
    public TimerResetCommand (CreatureLogic cl)
	{
		this.cl = cl;

	}

	public override void StartCommandExecution()
	{
		cl.timer.ResetTurn();
		Command.CommandExecutionComplete();	
	}
}
