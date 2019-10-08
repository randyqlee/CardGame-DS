using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPreAttackSequenceCommand : Command

{
    CreatureLogic attacker;
    CreatureLogic target;

	public StartPreAttackSequenceCommand (CreatureLogic attacker, CreatureLogic target)
	{
		this.attacker = attacker;
		this.target = target;

	}
	public override void StartCommandExecution()
	{
		attacker.PreAttack(target);

		Command.CommandExecutionComplete();
		
	}
}
