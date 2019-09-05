using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCooldownCommand : Command {

	AbilityCard abilityCard;
	int cooldown;
	int originalCooldown;

	public UpdateCooldownCommand (AbilityCard abilityCard, int cooldown, int originalCooldown)
	{
		this.abilityCard = abilityCard;
		this.cooldown = cooldown;
		this.originalCooldown = originalCooldown;

	}

	public override void StartCommandExecution()
	{
		if(abilityCard!=null)
		abilityCard.UpdateCooldown(cooldown,originalCooldown);
		Command.CommandExecutionComplete();	
	}

}
