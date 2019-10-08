using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveCommand : Command {

	public CreatureLogic creature;

	public ReviveCommand (CreatureLogic creature)
	{
		this.creature = creature;
	}

	// Use this for initialization
	public override void StartCommandExecution()
	{

		creature.Revive();
		
		Command.CommandExecutionComplete();
		

	}
	

}