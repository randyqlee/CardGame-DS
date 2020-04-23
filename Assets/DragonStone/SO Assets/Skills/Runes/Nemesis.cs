using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nemesis : CreatureEffect
{

    public string buffName = "Invincibility";
    public int buffCD = 1;

    // Start is called before the first frame update
    public Nemesis(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        //make the creature invincible one time at start of game
        AddBuff(creature,buffName,buffCD);
        
    }
}

