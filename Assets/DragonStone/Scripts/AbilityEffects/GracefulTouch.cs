using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GracefulTouch : CreatureEffect {

    public int buffCooldown = 1;
    int chance = 60;

    public GracefulTouch(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
       
    }

    public override void UseEffect(CreatureLogic target)
    {
        
            if(remainingCooldown <=0)
            {
                AddBuff(target,"CrippledStrike",buffCooldown);  
                
                int totalChance = TotalChance(chance);
                
                if(Random.Range(0,100)<totalChance)
                AddBuff(creature,"CriticalStrike",buffCooldown);                     
                base.UseEffect();   
                //AddBuff(target,"Bomb",buffCooldown);         

            }       
    }

}
