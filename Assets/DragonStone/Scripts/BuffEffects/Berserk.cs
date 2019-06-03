﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : BuffEffect {
   
    //Dictionary<int, CreatureLogic> enemies = new Dictionary<int, CreatureLogic>();
    List<CreatureLogic> enemies = new List<CreatureLogic>();
	
    public Berserk(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { isDebuff = true;}

    public override void CauseBuffEffect()
    {           
        target.e_CreatureOnTurnStart += berserkEffect;
    }

    public override void UndoBuffEffect()
    {
        target.e_CreatureOnTurnStart -= berserkEffect;
    }

    public void berserkEffect()
    {
       
        new DelayCommand(1.5f).AddToQueue();
       target.AttackCreatureWithID(attackRandomEnemy());
       new DelayCommand(1.5f).AddToQueue();

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
      return randomEnemy.UniqueCreatureID;

    }//attack
    
    
}//DecreaseAttack Method