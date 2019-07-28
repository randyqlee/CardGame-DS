using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SequentialAttack : CreatureEffect {

    public int buffCooldown = 1;
    public int chance = 100;

    public SequentialAttack(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_BeforeAttacking += UseEffect;      
       creature.e_AfterAttacking += UseEffect2;      
       
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_BeforeAttacking -= UseEffect;     
        creature.e_AfterAttacking -= UseEffect2;       
        
    }

    public override void CauseEventEffect()
    {
    //    if(remainingCooldown <=0)
    //     Debug.Log("Activate Effect: " +this.ToString());
    }

    public override void UseEffect(CreatureLogic target)
    {                      
        if(remainingCooldown<=0)
        {               
            ShowAbility();

            if(Random.Range(0,100) <= TotalChance(chance))
            AddBuff(creature,"CriticalStrike",buffCooldown);  

            base.UseEffect();                                

            
        }        
        
    }

    void UseEffect2(CreatureLogic target)
    {
        if(remainingCooldown <= 0)
        {
            AddBuff(target,"Brand",buffCooldown);
        }
        
       
    }

    

}
