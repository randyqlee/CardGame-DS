using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBuffPreviewCommand : Command {

	private BuffEffect be;
	private int TargetUniqueID;
	private string text;

	public ShowBuffPreviewCommand (BuffEffect be, int TargetUniqueID, string text)
	{
		this.be = be;
		this.TargetUniqueID = TargetUniqueID;
		this.text = text;

	}

	// Use this for initialization
	public override void StartCommandExecution()
	{

		//Debug.Log ("ShowSkillPreviewCommand");
		GameObject creature = IDHolder.GetGameObjectWithID(TargetUniqueID);

		creature.GetComponentInChildren<OverheadText>().FloatingText(text);

		
		//Command.CommandExecutionComplete();
		//		StartCoroutine(Delay());
		

	}
	

}
