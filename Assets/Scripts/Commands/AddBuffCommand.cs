using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuffCommand : Command {

	private BuffEffect buffEffect;
	private int TargetUniqueID;
	//private int AttackerUniqueID;


	public AddBuffCommand(BuffEffect buffEffect, int TargetUniqueID)

	{
		Debug.Log ("Call AddBuffCommand");
		this.buffEffect = buffEffect;
		this.TargetUniqueID = TargetUniqueID;

	}

	public override void StartCommandExecution()
	{
		Debug.Log ("StartCommand AddBuffCommand");
		GameObject creature = IDHolder.GetGameObjectWithID(TargetUniqueID);
		BuffPanel buffPanel = creature.GetComponentInChildren<BuffPanel>();
		

		buffPanel.AddBuffItem(buffEffect);
		Command.CommandExecutionComplete();

	}

}
