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
        target.e_CreatureOnTurnStart += stunEffect;
        
    }

    public override void UndoBuffEffect()
    {
        target.e_CreatureOnTurnStart -= stunEffect;
    }

    public void stunEffect()
    {
        Debug.Log("This creature is stunned: " +target.UniqueCreatureID);
        //TurnManager.Instance.EndTurn();

        new EndTurnCommand().AddToQueue();
        target.OnTurnEnd();
    }
    
    
}//DecreaseAttack Method
