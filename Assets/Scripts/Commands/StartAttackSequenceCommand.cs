using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAttackSequenceCommand : Command
{
    CreatureLogic attacker;
    CreatureLogic target;

	public StartAttackSequenceCommand (CreatureLogic attacker, CreatureLogic target)
	{
		this.attacker = attacker;
		this.target = target;

	}
	public override void StartCommandExecution()
	{
		attacker.AttackCreature(target);

		Command.CommandExecutionComplete();
		
	}
}
