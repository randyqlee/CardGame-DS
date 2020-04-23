using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Will : CreatureEffect
{

    public string buffName = "Immunity";
    public int buffCD = 3;

    // Start is called before the first frame update
    public Will(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        AddBuff(creature,buffName,buffCD);
        
    }
}

