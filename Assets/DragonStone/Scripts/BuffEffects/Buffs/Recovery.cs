using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : BuffEffect {

    public int healValue = 4;
    
	
    public Recovery(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Recovery");
        isBuff = true;}

    public override void CauseBuffEffect()
    {
        
        //target.e_CreatureOnTurnStart += healEffect;
        //target.e_PreAttackEvent += healEffect;
        TurnManager.Instance.e_ResetRound += healEffect;
        
    }

    public override void UndoBuffEffect()
    {
        
         //target.e_CreatureOnTurnStart -= healEffect;
         //target.e_PreAttackEvent -= healEffect;
         TurnManager.Instance.e_ResetRound -= healEffect;
    }

    public void healEffect()
    {
        new DelayCommand(0.5f).AddToQueue();
        target.Heal(healValue);
       
    }

    
    
}//DecreaseAttack Method
