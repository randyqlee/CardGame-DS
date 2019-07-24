using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class ArchangelsBlessing : CreatureEffect {

    public int buffCooldown = 1;
    CreatureLogic weakestAlly;
    List<CreatureLogic> temp;
    

    public ArchangelsBlessing(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }


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
            
            temp = owner.AllyList();
            weakestAlly = creature;   
            
            foreach(CreatureLogic cl in temp)
            {            
               
                if(cl.Health < weakestAlly.Health)
                weakestAlly = cl;
                //Debug.Log("Ally Creature: " +cl.GetType().Name);               
                         
            }

            Debug.Log("Allies List Size " +temp.ToString());
            HealWeakestAlly(weakestAlly);

              base.UseEffect();    

              

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
