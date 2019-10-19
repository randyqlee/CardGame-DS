using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DarkRecovery : CreatureEffect
{   

    public bool stayActive = false;

    public DarkRecovery(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
        creature.e_PreAttackEvent += UseEffect; 
        creature.e_CreatureOnTurnEnd += GainExtraTurn;        
       
      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_PreAttackEvent -= UseEffect;
         creature.e_CreatureOnTurnEnd -= GainExtraTurn;
         
         
    }

    public override void UseEffect(CreatureLogic target)
    {
        
        if(creatureEffectCooldown <= 0)
        {
            ShowAbility();
            stayActive = false;

           
            int damage = target.MaxHealth / 2;
            new DealDamageCommand(target.ID, damage, healthAfter: target.TakeOtherDamageVisual(damage), armorAfter:target.TakeArmorDamageVisual(damage)).AddToQueue();
            target.TakeOtherDamage (damage);

            creature.Heal(damage);
            

            if(target.isDead)
            {
                stayActive = true;
            }

            base.UseEffect();


        }
    }
    

    public void GainExtraTurn()
    {
        if (stayActive)
        {
            creature.ExtraTurn();
            
            stayActive = false; 
        }      
    }

   
}
