using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class ArchangelsBlessing : CreatureEffect {

    public int buffCooldown = 1;

    public ArchangelsBlessing(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_CreatureOnTurnStart += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_CreatureOnTurnStart -= UseEffect;      
    }

    public override void CauseEventEffect()
    {
       
    }

    public override void UseEffect(CreatureLogic target)
    {     
        if(remainingCooldown <=0)
        {     
          
        }

        base.UseEffect();         
    }

    public void HealWeakestAlly(CreatureLogic target)
    {
        
    }



}
