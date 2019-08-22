using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSkillPreviewCommand : Command {

	private CreatureEffect ce;
	private int TargetUniqueID;
	private string text;

	public ShowSkillPreviewCommand (CreatureEffect ce, int TargetUniqueID, string text)
	{
		this.ce = ce;
		this.TargetUniqueID = TargetUniqueID;
		this.text = text;

	}
	// Use this for initialization
	public override void StartCommandExecution()
	{

		Debug.Log ("ShowSkillPreviewCommand");
		GameObject creature = IDHolder.GetGameObjectWithID(TargetUniqueID);

		if(creature.tag == "TopCreature")
		creature.GetComponentInChildren<OverheadText>().ShowSkillPreview(ce);

		
		creature.GetComponentInChildren<OverheadText>().FloatingText(text);

		
		//Command.CommandExecutionComplete();
		//		StartCoroutine(Delay());
		

	}
	

}
