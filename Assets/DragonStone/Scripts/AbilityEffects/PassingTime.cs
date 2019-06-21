using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PassingTime : CreatureEffect {

    public int buffCooldown = 0;
    int criticalChance = 75;

    public PassingTime(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_CreatureOnTurnStart += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_CreatureOnTurnEnd -= UseEffect;      
    }

    public override void CauseEventEffect()
    {
       
    }

    public override void UseEffect()
    {        
        int x = Random.Range(0,101);

        if(x<=criticalChance)
        {
            AddBuff(creature,"CriticalStrike",buffCooldown);               
            base.UseEffect();          
        }        
        
    }
}
