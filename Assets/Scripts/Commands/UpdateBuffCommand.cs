using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBuffCommand : Command {

	private BuffEffect buffEffect;
	private int buffCooldown;
	public UpdateBuffCommand(BuffEffect buffEffect)
	{
		//Debug.Log ("Call UpdateBuffCommand");
		this.buffEffect = buffEffect;
		this.buffCooldown = buffEffect.buffCooldown;
	}

	public override void StartCommandExecution()
	{
		//Debug.Log ("StartCommand UpdateBuffCommand");
		
		GameObject buffImage = IDHolder.GetGameObjectWithID(buffEffect.buffID);

		buffImage.GetComponentInChildren<Text>().text = buffEffect.buffCooldown.ToString();
		
		Command.CommandExecutionComplete();

	}

}
