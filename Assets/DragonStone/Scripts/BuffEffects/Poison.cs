using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : BuffEffect {

    public int poisonDamage = 2;
    
	
    public Poison(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { isDebuff = true;}

    public override void CauseBuffEffect()
    {
        //target.e_CreatureOnTurnStart += DealPoisonDamage;
        target.e_CreatureOnTurnEnd += DealPoisonDamage;
        
    }

    public override void UndoBuffEffect()
    {
        //target.e_CreatureOnTurnStart -= DealPoisonDamage;
        target.e_CreatureOnTurnEnd -= DealPoisonDamage;
    }

    public void DealPoisonDamage()
    {
        new DelayCommand(0.5f).AddToQueue();
        new DealDamageCommand(target.ID, poisonDamage, healthAfter: target.Health - poisonDamage).AddToQueue();

        target.Health -= poisonDamage;    
        Debug.Log("Poison Activated");
    }

    
    
}//DecreaseAttack Method
