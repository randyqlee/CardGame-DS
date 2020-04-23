using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : CreatureEffect
{

    public int value = 25;
    // Start is called before the first frame update
    public Energy(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        creature.MaxHealth += value;
        creature.Health += value;
    }



}