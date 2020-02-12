using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFindValidTargets : MonoBehaviour
{
    private OneCreatureManager manager;

    private CreatureLogic creature;
    private Player owner;

    void Awake()
    {
        manager = GetComponentInParent<OneCreatureManager>();
    }
    void Start()
    {
        
   }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnMouseDown()
    {
        creature = CreatureLogic.CreaturesCreatedThisGame[GetComponentInParent<IDHolder>().UniqueID];
        owner = creature.owner;

        if(creature.CanAttack)
        {
            TargetSystem.Instance.ClearAllEnemiesAsTargets(owner);
            TargetSystem.Instance.FindValidTargetsForThisCreature(creature);
        }

        //call here the class and method to determine valid targets for the creature

    }


    void OnMouseUp()
    {
        //DO NOTHING

    }   
}
