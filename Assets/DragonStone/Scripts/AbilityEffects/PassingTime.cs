using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PassingTime : CreatureEffect {

    public int buffCooldown = 0;
    int chance = 100;

    public PassingTime(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_CreatureOnTurnEnd += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_CreatureOnTurnEnd -= UseEffect;      
    }

    public override void CauseEventEffect()
    {
       
    }

    public override void UseEffect(CreatureLogic target)
    {        
        
        if(remainingCooldown<=0)
        {
            //AddBuff(creature,"CriticalStrike",buffCooldown);    

            //if(Random.Range(0,100)<chance)           
            //SecondAttack(target);

            base.UseEffect();

        }        
        
    }

    void SecondAttack(CreatureLogic target)
    {               
        creature.AttacksLeftThisTurn++;
        creature.AttackCreature(target);
    }
}
