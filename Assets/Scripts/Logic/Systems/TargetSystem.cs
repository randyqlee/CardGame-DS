using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{

    public static TargetSystem Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClearAllEnemiesAsTargets(Player owner)
    {
        foreach(CreatureLogic cl in owner.EnemyList())
        {
            cl.IsValidTarget = false;
        }
    }

    public void FindValidTargetsForThisCreature (CreatureLogic creature)
    {
        List<CreatureLogic> validTargets = new List<CreatureLogic>();
        bool tauntCreatureFound = false;
        foreach(CreatureLogic cl in creature.owner.EnemyList())
        {
            cl.canBeAttacked = false;
            foreach(BuffEffect be in cl.buffEffects)
            {
                if (be.Name == "Taunt")
                {
                    validTargets.Add(cl);
                    tauntCreatureFound = true;
                    break;
                }
            }
        }

        if (tauntCreatureFound)
        {

        }
        else
        {
            foreach(CreatureLogic cl in creature.owner.EnemyList())
            {
                validTargets.Add(cl);
            }
        }

        AssignValidTargets(validTargets);
    }

    public void AssignValidTargets (List<CreatureLogic> validTargets)
    {
        foreach(CreatureLogic cl in validTargets)
        {
            cl.IsValidTarget = true;
            cl.canBeAttacked = true;
        }
    }

}
