using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffEffect : CreatureEffect {

    public int buffCooldown;
    public BuffEffect(Player owner, CreatureLogic creature, int buffCooldown): base(owner, creature, buffCooldown)
    {
        this.creature = creature;
        this.owner = owner;
        this.buffCooldown = buffCooldown;

    }

}
