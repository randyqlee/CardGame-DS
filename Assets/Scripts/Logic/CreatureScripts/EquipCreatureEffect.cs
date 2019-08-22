using UnityEngine;
using System.Collections;

public class EquipCreatureEffect : CreatureEffect
{
    public EquipCreatureEffect(Player owner, CreatureLogic creature) : base(owner, creature)
    {}

    // BATTLECRY
    public override void WhenACreatureIsPlayed()
    {
        Debug.Log ("Playing Equip Creature: " + owner + "," + creature.ca.name);
    }
}
