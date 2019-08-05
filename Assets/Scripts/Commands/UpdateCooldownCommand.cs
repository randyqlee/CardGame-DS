using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCooldownCommand : Command {

	AbilityCard abilityCard;
	int cooldown;

	public UpdateCooldownCommand (AbilityCard abilityCard, int cooldown)
	{
		this.abilityCard = abilityCard;
		this.cooldown = cooldown;

	}

	public override void StartCommandExecution()
	{
		abilityCard.UpdateCooldown(cooldown);
		Command.CommandExecutionComplete();	
	}

}
