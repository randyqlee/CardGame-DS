using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class HeavenlySword : CreatureEffect {

    public int buffCooldown = 1;
    int skillDamage = 5;

    public HeavenlySword(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_AfterAttacking += UseEffect;
       creature.e_BeforeAttacking += ShowAbility;
             
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_AfterAttacking -= UseEffect;      
        creature.e_BeforeAttacking -= ShowAbility;
    }

    public override void CauseEventEffect()
    {
    //    if(remainingCooldown <=0)
    //     Debug.Log("Activate Effect: " +this.ToString());
    }

    public override void UseEffect(CreatureLogic target)
    {                   
        target.RemoveRandomBuff();            
        base.UseEffect();    
            
    }

    // public void RemoveRandomBuff(CreatureLogic target)
    // {
    //     var randList = new List<BuffEffect>();

    //     foreach(BuffEffect be in target.buffEffects)
    //     {
    //         if(be.isBuff)
    //         randList.Add(be);
    //     }

    //     if(randList.Count>=1)
    //     {
    //         randList[Random.Range(0,randList.Count)].RemoveBuff();
    //         target.TakeDamage(skillDamage);
    //     }
       
    // }



}
