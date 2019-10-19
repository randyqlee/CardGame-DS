using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Serenity : CreatureEffect {

    public int buffCooldown = 2;
    public int heal = 7;

    public Serenity(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_AfterAttacking += UseEffect;        
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_AfterAttacking -= UseEffect;      
    }


    public override void UseEffect(CreatureLogic target)
    {                      
        if(remainingCooldown<=0)
        {
            
            ShowAbility();
            foreach(CreatureLogic cl in owner.AllyList())
            {
                   

                    //Temp
                    // cl.Health += heal;
                    // new UpdateHealthCommand(cl.ID, cl.Health).AddToQueue();

                    //Temp
                    cl.Heal(heal);

                    AddBuff(cl, "Lucky", buffCooldown);
                    AddBuff(cl, "Immunity", buffCooldown);
            }
            base.UseEffect();                             
             
        }
        
        
    }

}
