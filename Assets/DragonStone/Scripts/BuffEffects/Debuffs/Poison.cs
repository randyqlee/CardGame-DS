using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : BuffEffect {

    public int poisonDamage = 3;
    
	
    public Poison(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Poison");
        isDebuff = true;}

    public override void CauseBuffEffect()
    {
        //target.e_CreatureOnTurnStart += DealPoisonDamage;
        //target.e_CreatureOnTurnEnd += DealPoisonDamage;
        TurnManager.Instance.e_EndOfRound += DealPoisonDamage;
        
    }

    public override void UndoBuffEffect()
    {
        //target.e_CreatureOnTurnStart -= DealPoisonDamage;
        //target.e_CreatureOnTurnEnd -= DealPoisonDamage;
        TurnManager.Instance.e_EndOfRound -= DealPoisonDamage;
    }

    public void DealPoisonDamage()
    {
        new DelayCommand(0.5f).AddToQueue();

        //new DealDamageCommand(target.ID, poisonDamage, healthAfter: target.Health - target.DealDamage(poisonDamage)).AddToQueue();
        new ShowBuffPreviewCommand(this, target.ID, this.GetType().Name).AddToQueue();
        new SfxExplosionCommand(target.UniqueCreatureID).AddToQueue();
        

        new DealDamageCommand(target.ID, poisonDamage, healthAfter: target.TakeOtherDamageVisual(poisonDamage), armorAfter: target.TakeArmorDamageVisual(poisonDamage)).AddToQueue();

        target.TakeOtherDamage(poisonDamage);    
        Debug.Log("Poison Activated" +target.UniqueCreatureID);
    }

    
    
}//DecreaseAttack Method
