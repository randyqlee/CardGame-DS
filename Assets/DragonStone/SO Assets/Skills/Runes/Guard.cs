using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : CreatureEffect
{

    // Start is called before the first frame update
    public Guard(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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

    public override void UseEffect(CreatureLogic target)
    {
        creature.permanentTaunt = true;
    }



}