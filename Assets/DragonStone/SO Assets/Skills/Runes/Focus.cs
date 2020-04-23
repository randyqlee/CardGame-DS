﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Focus : CreatureEffect
{

    public int value = 10;
    // Start is called before the first frame update
    public Focus(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
       creature.e_ActivateRune  += UseEffect;      
 
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_ActivateRune -= UseEffect;  
         
    }

    public override void UseEffect()
    {
        creature.chance += value;
    }



}
