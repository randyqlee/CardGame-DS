using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class SweetDreams : CreatureEffect {

    public int buffCooldown = 1;

    public SweetDreams(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_AfterAttacking -= UseEffect;      
    }

    public override void CauseEventEffect()
    {
       if(remainingCooldown <=0)
        Debug.Log("Activate Effect: " +this.ToString());
    }

    public override void UseEffect(CreatureLogic target)
    {     
        if(remainingCooldown <=0)
        {     
           RemoveAllBuffs(target);            
           AddBuff(target,"Stun",buffCooldown);   
           base.UseEffect();                       
        }

        
    }

    public void RemoveAllBuffs(CreatureLogic target)
    {
        int i = target.buffEffects.Count;
        for(int x = i-1; x>=0; x--)
        {
            if(target.buffEffects[x].isBuff)
            target.buffEffects[x].RemoveBuff();
        }
    }



}
