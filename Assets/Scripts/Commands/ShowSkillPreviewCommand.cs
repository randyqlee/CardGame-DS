using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSkillPreviewCommand : Command {

	private CreatureEffect ce;
	private int TargetUniqueID;

	public ShowSkillPreviewCommand (CreatureEffect ce, int TargetUniqueID)
	{
		this.ce = ce;
		this.TargetUniqueID = TargetUniqueID;

	}
	// Use this for initialization
	public override void StartCommandExecution()
	{

		Debug.Log ("ShowSkillPreviewCommand");
		GameObject creature = IDHolder.GetGameObjectWithID(TargetUniqueID);
		creature.GetComponentInChildren<OverheadText>().ShowSkillPreview(ce);
		
		//CommandExecutionComplete();
		//		StartCoroutine(Delay());
		

	}
	

}
