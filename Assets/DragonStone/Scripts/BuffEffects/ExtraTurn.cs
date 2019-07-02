using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraTurn : BuffEffect {

    //int attackModifier = 5;
    
	
    public ExtraTurn(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Invincibility");
        isBuff = true;}

    public override void CauseBuffEffect()
    {
        
        target.e_CreatureOnTurnEnd += Extraturn;
    }

    public override void UndoBuffEffect()
    {
        target.e_CreatureOnTurnEnd -= Extraturn;
    }

    public void Extraturn()
    {
        TurnManager.Instance.TurnCounter++;
        target.owner.ExtraCreatureTurn = 1;

        Debug.Log("Extra Turn! ");
    }



    
    
}//DecreaseAttack Method
