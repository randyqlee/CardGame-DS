using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : BuffEffect {
   
    int bombDamage = 10;
    int bombCooldown;
	
    public Bomb(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { 
        buffIcon = Resources.Load<Sprite>("BuffIcons/Bomb");
        isDebuff = true;
        bombCooldown = buffCooldown;
        }

    public override void CauseBuffEffect()
    {        
        //target.e_CreatureOnTurnEnd += bombEffect;
        TurnManager.Instance.e_EndOfRound += bombEffect;
        TurnManager.Instance.e_ResetRound += stunEffect;
    }

    public override void UndoBuffEffect()
    {
        //target.e_CreatureOnTurnEnd -= bombEffect;
         TurnManager.Instance.e_EndOfRound -= bombEffect;
         TurnManager.Instance.e_ResetRound -= stunEffect;
    }

    public void bombEffect()
    {        
        bombCooldown--;
        if(bombCooldown <= 0)
        {
            
           new DelayCommand(0.5f).AddToQueue();

            //new DealDamageCommand(target.ID, poisonDamage, healthAfter: target.Health - target.DealDamage(poisonDamage)).AddToQueue();

            new DealDamageCommand(target.ID, bombDamage, healthAfter: target.TakeOtherDamageVisual(bombDamage), armorAfter: target.TakeArmorDamageVisual(bombDamage)).AddToQueue();
            
            
            target.TakeOtherDamage(bombDamage);
            
            Debug.Log("BOOM! " +target.UniqueCreatureID);
            //base.AddBuff(target, "Stun", 2);
           
            //TurnManager.Instance.EndTurn();

    
        }
        
    }

    public void stunEffect()
    {
          if(bombCooldown <= 0)
          base.AddBuff(target, "Stun", 1);
    }

    
    
}//DecreaseAttack Method
