using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBuffCommand : Command {

	private BuffEffect buffEffect;
	private int TargetUniqueID;
	//private int AttackerUniqueID;


	public DestroyBuffCommand(BuffEffect buffEffect,int TargetUniqueID)

	{
		//Debug.Log ("Call DestroyBuffCommand");
		this.buffEffect = buffEffect;
		this.TargetUniqueID = TargetUniqueID;

	}

	public override void StartCommandExecution()
	{
		//Debug.Log ("StartCommand DestroyBuffCommand");
		GameObject creature = IDHolder.GetGameObjectWithID(TargetUniqueID);
		BuffPanel buffPanel = creature.GetComponentInChildren<BuffPanel>();
		//buffPanel.DestroyBuffwithID(buffEffect.buffID);

		GameObject buff = IDHolder.GetGameObjectWithID(buffEffect.buffID);
		buffPanel.DestroyBuff(buff);
		
		

		Command.CommandExecutionComplete();

	}

}
