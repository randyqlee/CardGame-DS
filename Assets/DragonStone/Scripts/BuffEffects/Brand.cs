using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brand : BuffEffect {

    int brandDamage = 2;
	
    public Brand(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    { isDebuff = true;}


    public override void CauseBuffEffect()
    {        
        target.e_IsAttacked += DealBrandDamage;
        
    }

    public override void UndoBuffEffect()
    {
        target.e_IsAttacked -= DealBrandDamage;
    }

    
     public void DealBrandDamage()
    {
        
        new DelayCommand(0.5f).AddToQueue();
        new DealDamageCommand(target.ID, brandDamage, healthAfter: target.Health - target.ComputeDamage(brandDamage, target)).AddToQueue();
        target.Health -= target.ComputeDamage(brandDamage, target);    
        Debug.Log("Brand Damage Activated, Health is " +target.Health.ToString());
    }
   
    
    
}//DecreaseAttack Method
