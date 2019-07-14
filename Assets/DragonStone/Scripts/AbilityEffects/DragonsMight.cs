using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class DragonsMight : CreatureEffect {

    public int buffCooldown = 1;
    public int buffCooldown2 = 0;
    public int chance = 100;

    public DragonsMight(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_AfterAttacking += UseEffect;
       creature.e_CreatureOnTurnStart += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_AfterAttacking -= UseEffect;      
         creature.e_CreatureOnTurnStart -= UseEffect;
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
                int x = Random.Range(0,101);

                if(x<=chance)     
                {
                   AddBuff(target,"Poison",buffCooldown);                    
                   base.UseEffect();         
                }       
                     
            }           
    }

    public override void UseEffect()
    {
            if(remainingCooldown<=0)
            {
                int x = Random.Range(0,101);
            
                if(x<=chance)
                {
                   AddBuff(creature,"CriticalStrike",buffCooldown2);

                   //Test
                   AddBuff(creature,"Invincibility",2);
                   base.UseEffect();          
                }            
                
            }       
            
    }
}
