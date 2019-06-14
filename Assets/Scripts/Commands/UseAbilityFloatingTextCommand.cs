using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbilityFloatingTextCommand : Command {


	private string text;

	private int TargetUniqueID;

	public UseAbilityFloatingTextCommand (string text, int TargetUniqueID)
	{
		Debug.Log ("Call UseAbilityFloatingTextCommand");
		this.text = text;
		this.TargetUniqueID = TargetUniqueID;

	}

	public override void StartCommandExecution()
	{
		Debug.Log ("StartCommand UseAbilityFloatingTextCommand");
		GameObject creature = IDHolder.GetGameObjectWithID(TargetUniqueID);
		creature.GetComponentInChildren<OverheadText>().FloatingText(text);
		Command.CommandExecutionComplete();
		

	}


}
