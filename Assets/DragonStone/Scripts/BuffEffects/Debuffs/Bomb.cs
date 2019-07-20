using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : BuffEffect {
   
    int bombDamage = 10;
	
    public Bomb(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { 
        buffIcon = Resources.Load<Sprite>("BuffIcons/Bomb");
        isDebuff = true;}

    public override void CauseBuffEffect()
    {        
        target.e_CreatureOnTurnEnd += bombEffect;
    }

    public override void UndoBuffEffect()
    {
        target.e_CreatureOnTurnEnd -= bombEffect;
    }

    public void bombEffect()
    {        
        if(buffCooldown <= 0)
        {
            
           new DelayCommand(0.5f).AddToQueue();

            //new DealDamageCommand(target.ID, poisonDamage, healthAfter: target.Health - target.DealDamage(poisonDamage)).AddToQueue();

            new DealDamageCommand(target.ID, bombDamage, healthAfter: target.TakeOtherDamageVisual(target.DealDamage(bombDamage))).AddToQueue();
            
            
            target.TakeOtherDamage(bombDamage);
            Debug.Log("BOOM! " +target.UniqueCreatureID);
            base.AddBuff(target, "Stun", 1);
           
            //TurnManager.Instance.EndTurn();

        }
        
    }

    
    
}//DecreaseAttack Method
