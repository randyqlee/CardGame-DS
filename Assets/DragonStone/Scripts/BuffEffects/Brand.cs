using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brand : BuffEffect {

    int brandDamage = 2;
	
    public Brand(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { isDebuff = true;}


    public override void CauseBuffEffect()
    {        
        target.e_CreatureIsAttacked += DealBrandDamage;
        
    }

    public override void UndoBuffEffect()
    {
        target.e_CreatureIsAttacked -= DealBrandDamage;
    }

    
     public void DealBrandDamage()
    {
        
        new DelayCommand(0.5f).AddToQueue();
        new DealDamageCommand(target.ID, brandDamage, healthAfter: target.Health - brandDamage).AddToQueue();
        target.Health -= brandDamage;    
        Debug.Log("Brand Damage Activated, Health is " +target.Health.ToString());
    }
   
    
    
}//DecreaseAttack Method
