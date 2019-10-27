using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transcendance : CreatureEffect
{

    public int buffCooldown = 1;
    List<CreatureLogic> enemyList = new List<CreatureLogic>();

    public Transcendance(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        //enemyList = owner.EnemyList();
        enemyList = owner.otherPlayer.AllyList();
    }

   public override void RegisterEventEffect()
    {

       
        foreach(CreatureLogic cl in enemyList)
        {
            
            //Debug.Log("enemy hero: " +cl.Name);
            cl.e_CreatureOnTurnEnd += UseEffect;
             
        }
            
    }

    public override void UnRegisterEventEffect()
    {
        foreach(CreatureLogic cl in enemyList)
        {
            cl.e_CreatureOnTurnEnd -= UseEffect;  
                 
        }
                
    }

    public override void UseEffect()
    {
        //if(CanUseAbility())
        //{ 
            Debug.Log("This skill: " +this.Name);

            if(ChanceOK(creature.chance) && creature.hasStun == false)
            {
                    ShowAbility();
                    AddBuff(creature, "IncreaseAttack", buffCooldown);
                    creature.ExtraTurn();
            }
                
            base.UseEffect();
        //}
    }
}