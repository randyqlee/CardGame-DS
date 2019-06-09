using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class ArchangelsBlessing : CreatureEffect {

    public int buffCooldown = 1;
    CreatureLogic weakestAlly;
    

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

    public override void UseEffect()
    {     
        if(remainingCooldown <=0)
        {           
            weakestAlly = creature;   
            foreach(CreatureLogic cl in owner.allies)
            {            
               
                if(cl.Health < weakestAlly.Health)
                weakestAlly = cl;
                         
            }

            HealWeakestAlly(weakestAlly);

              base.UseEffect();    

              Debug.Log("Archangels Blessing");

        }           

    }//UseEffect

    public void HealWeakestAlly(CreatureLogic target)
    {
        //owner.allies
       int healAmount;
       healAmount = Mathf.Abs(target.MaxHealth - target.Health);
       target.Heal(healAmount);
    }

}
