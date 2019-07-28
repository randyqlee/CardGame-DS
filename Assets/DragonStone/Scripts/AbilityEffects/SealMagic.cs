using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class SealMagic : CreatureEffect {

  

    public SealMagic(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_BeforeAttacking += ShowAbility;      
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_BeforeAttacking -= ShowAbility;      
        creature.e_AfterAttacking -= UseEffect;      
    }

    public override void CauseEventEffect()
    {
    //    if(remainingCooldown <=0)
    //     Debug.Log("Activate Effect: " +this.ToString());
    }

    public override void UseEffect(CreatureLogic target)
    {     
        if(remainingCooldown <=0)
        {     
           
           ResetCooldown(target);                      
           base.UseEffect();
           Extraturn();               
        }

              
    }

    public void ResetCooldown(CreatureLogic target)
    {        

        foreach(CreatureEffect ce in target.creatureEffects)
        {
            ce.remainingCooldown = ce.creatureEffectCooldown;
        }
        
    }

    public void Extraturn()
    {
        TurnManager.Instance.TurnCounter++;
        owner.ExtraCreatureTurn = 1;

        Debug.Log("Seal Magic Extra Turn! ");
    }



}
