using UnityEngine;
using System.Collections;

public class CreatureAttackCommand : Command 
{
    // position of creature on enemy`s table that will be attacked
    // if enemyindex == -1 , attack an enemy character 
    private int TargetUniqueID;
    private int AttackerUniqueID;
    private int AttackerHealthAfter;
    private int TargetHealthAfter;
    private int TargetArmorAfter;
    private int DamageTakenByAttacker;
    private int DamageTakenByTarget;
    private bool CanAttack;

    public CreatureAttackCommand(int targetID, int attackerID, int damageTakenByAttacker, int damageTakenByTarget, int attackerHealthAfter, int targetHealthAfter, int targetArmorAfter, bool canAttack)
    {
        //Debug.Log ("Call CreatureAttackCommand");
        this.TargetUniqueID = targetID;
        this.AttackerUniqueID = attackerID;
        this.AttackerHealthAfter = attackerHealthAfter;
        this.TargetHealthAfter = targetHealthAfter;
        this.TargetArmorAfter = targetArmorAfter;
        this.DamageTakenByTarget = damageTakenByTarget;
        this.DamageTakenByAttacker = damageTakenByAttacker;
        this.CanAttack = canAttack;
    }

    public override void StartCommandExecution()
    {
        //Debug.Log ("StartCommand CreatureAttackCommand");
        GameObject Attacker = IDHolder.GetGameObjectWithID(AttackerUniqueID);
        GameObject Target = IDHolder.GetGameObjectWithID(TargetUniqueID);

        //check first if gameobjects still exist, to account for possible delays
        if (Attacker != null && Target != null)
        {
            //Debug.Log(TargetUniqueID);
            Attacker.GetComponent<CreatureAttackVisual>().AttackTarget(TargetUniqueID, DamageTakenByTarget, DamageTakenByAttacker, AttackerHealthAfter, TargetHealthAfter, TargetArmorAfter, AttackerUniqueID, CanAttack);
        }
    }
}
