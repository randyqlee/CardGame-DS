using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffect : CreatureEffect {

    public BuffEffect(Player owner, CreatureLogic creature, int buffCooldown): base(owner, creature, buffCooldown)
    {}

}
