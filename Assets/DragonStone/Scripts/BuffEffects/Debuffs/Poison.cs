using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : BuffEffect {

    public int poisonDamage = 2;
    
	
    public Poison(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Poison");
        isDebuff = true;}

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

        //new DealDamageCommand(target.ID, poisonDamage, healthAfter: target.Health - target.DealDamage(poisonDamage)).AddToQueue();

        new DealDamageCommand(target.ID, poisonDamage, healthAfter: target.TakeOtherDamageVisual(target.DealDamage(poisonDamage))).AddToQueue();

        target.TakeOtherDamage(target.DealDamage(poisonDamage));    
        Debug.Log("Poison Activated" +target.UniqueCreatureID);
    }

    
    
}//DecreaseAttack Method
