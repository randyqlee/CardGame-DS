using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : BuffEffect {
   
    
	
    public Stun(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { 
        buffIcon = Resources.Load<Sprite>("BuffIcons/Stun");
        isDebuff = true;}

    public override void CauseBuffEffect()
    {        
        //target.e_CreatureOnTurnStart += stunEffect;
        StunEffect();
        
    }

    public override void UndoBuffEffect()
    {
        //target.e_CreatureOnTurnStart -= stunEffect;
        UndoStunEffect();
    }

    public void StunEffect()
    {
        //Debug.Log("This creature is stunned: " +target.UniqueCreatureID);
        //TurnManager.Instance.EndTurn();

        //new EndTurnCommand().AddToQueue();        
        target.OnTurnEnd();
        
        target.attackTurnModifier = -1*target.attacksForOneTurn;
        target.isActive = false;
        //target.AttacksLeftThisTurn = 0;
      
    }

    public void UndoStunEffect()
    {
        target.attackTurnModifier = 0;
    }
    
    
}//DecreaseAttack Method
