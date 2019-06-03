using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBuffCommand : Command {

	private BuffAsset buff;
	private int buffCooldown;
	private int TargetUniqueID;
	//private int AttackerUniqueID;


	public AddBuffCommand(BuffAsset buff, int buffCooldown, int TargetUniqueID)

	{
		this.buff = buff;
		this.buffCooldown = buffCooldown;
		this.TargetUniqueID = TargetUniqueID;

	}

	public override void StartCommandExecution()
	{
		GameObject creature = IDHolder.GetGameObjectWithID(TargetUniqueID);
		BuffPanel buffPanel = creature.GetComponentInChildren<BuffPanel>();
		

		//IDHolder id = .AddComponent<IDHolder>();
        //id.UniqueID = UniqueID;

	}

}
