using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : BuffEffect {
   
    //Dictionary<int, CreatureLogic> enemies = new Dictionary<int, CreatureLogic>();
    List<CreatureLogic> enemies = new List<CreatureLogic>();
	
    public Berserk(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Berserk");
        isDebuff = true;}

    public override void CauseBuffEffect()
    {           
        //target.e_CreatureOnTurnStart += berserkEffect;
        target.e_PreAttackEvent += berserkEffect;
        target.e_AfterAttacking += resetEffect;
    }

    public override void UndoBuffEffect()
    {
        //target.e_CreatureOnTurnStart -= berserkEffect;
        target.e_PreAttackEvent -= berserkEffect;
        target.e_AfterAttacking -= resetEffect;
    }

    public void resetEffect(CreatureLogic cl)
    {
        target.e_PreAttackEvent += berserkEffect;
    }

    public void berserkEffect(CreatureLogic cl)
    {
       
        //new DelayCommand(1.5f).AddToQueue();
        target.pauseAttack = true;
       target.e_PreAttackEvent -= berserkEffect;

 
       target.AttackCreatureWithID(attackRandomEnemy());
       
       
        //TurnManager.Instance.EndTurn();
    }

    public int attackRandomEnemy()
    {

      foreach(CreatureLogic ce in target.owner.EnemyList())
      {
          if(!ce.isDead)
           enemies.Add(ce);
      }      

      CreatureLogic randomEnemy = enemies[Random.Range(0,enemies.Count)];
      return randomEnemy.ID;

    }//attack
    
    
}//DecreaseAttack Method
