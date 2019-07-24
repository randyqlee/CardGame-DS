using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAbilityFloatingTextCommand : Command {


	private string text;

	private int TargetUniqueID;

	public UseAbilityFloatingTextCommand (string text, int TargetUniqueID)
	{
		//Debug.Log ("Call UseAbilityFloatingTextCommand");
		this.text = text;
		this.TargetUniqueID = TargetUniqueID;

	}

	public override void StartCommandExecution()
	{

		Debug.Log ("StartCommand UseAbilityFloatingTextCommand");
		GameObject creature = IDHolder.GetGameObjectWithID(TargetUniqueID);
		creature.GetComponentInChildren<OverheadText>().FloatingText(text);
		//CommandExecutionComplete();
		//		StartCoroutine(Delay());
		

	}
/*	IEnumerator Delay()
	{
		GameObject creature = IDHolder.GetGameObjectWithID(TargetUniqueID);
		creature.GetComponentInChildren<OverheadText>().FloatingText(text);
		yield return new WaitWhile(() => creature.GetComponentInChildren<OverheadText>().GetComponent<FloatingText>() != null);
		Command.CommandExecutionComplete();
	}
*/

	public override void Requeue()
	{
		new UseAbilityFloatingTextCommand(text, TargetUniqueID).AddToQueue();
		CommandExecutionComplete();

	}

}
