using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureGlowCommand : Command {

	public Image CreatureGlowImage;
	public bool value;
	public CreatureGlowCommand (Image CreatureGlowImage, bool value)
	{
		this.CreatureGlowImage = CreatureGlowImage;
		this.value = value;

	}
	public override void StartCommandExecution()
	{
		CreatureGlowImage.enabled = value;

		Command.CommandExecutionComplete();
		
	}
}
