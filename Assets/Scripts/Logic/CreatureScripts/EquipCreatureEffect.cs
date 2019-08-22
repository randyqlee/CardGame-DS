using UnityEngine;
using System.Collections;

public class EquipCreatureEffect : CreatureEffect
{
    public EquipCreatureEffect(Player owner, CreatureLogic creature) : base(owner, creature)
    {

        this.owner = owner;
        this.creature = creature;
    }

    // BATTLECRY
    public override void WhenACreatureIsPlayed()
    {

        foreach (CreatureLogic cl in owner.AllyList())
        {
            if(!cl.isEquip)
            {
                cl.Attack += 10;
                new UpdateAttackCommand(cl.ID, cl.Attack).AddToQueue();

                Debug.Log ("Attack +" + cl.Attack + " to " + cl.ca.name);
            }

        }
        
    }
}
