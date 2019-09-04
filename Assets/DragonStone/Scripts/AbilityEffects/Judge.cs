using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class Judge : CreatureEffect {

    public int buffCooldown = 1;
    public int brandDamage;

    public Judge(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        brandDamage = creature.DealDamage(creature.Attack);
    }


   public override void RegisterEventEffect()
    {
       creature.e_AfterAttacking += UseEffect;      
       //creature.e_BeforeAttacking += ShowAbility;
       creature.e_IsAttacked += Retaliate;      
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_AfterAttacking -= UseEffect;      
         //creature.e_BeforeAttacking -= ShowAbility;
         creature.e_IsAttacked -= Retaliate;        
    }

    public override void CauseEventEffect()
    {
       if(remainingCooldown <=0)
        Debug.Log("Activate Effect: " +this.ToString());
    }

    public override void UseEffect(CreatureLogic target)
    {     
        if(Random.Range(0,100)<=creature.chance)
        if(remainingCooldown <=0)
        {               
           ShowAbility(target);          
           RemoveBuff(target);                     
           base.UseEffect();                  
                
        }        
    }

    public void RemoveBuff(CreatureLogic target)
    {
        
        if(target.buffEffects.Count>0)
        {
            int index = Random.Range(0, target.buffEffects.Count-1);
            target.buffEffects[index].RemoveBuff();
        }  
    }

    public void Retaliate(CreatureLogic target)
    {
        
        if(Random.Range(0,100)<=creature.chance)
        {
            //new DelayCommand(0.5f).AddToQueue();           

            //new SfxExplosionCommand(target.UniqueCreatureID).AddToQueue();

            ShowAbility(creature);          
            new DealDamageCommand(target.ID, brandDamage, healthAfter: target.TakeOtherDamageVisual(target.DealOtherDamage(brandDamage))).AddToQueue();               

            //target.TakeOtherDamage(target.DealOtherDamage(brandDamage));    
            target.TakeOtherDamage(brandDamage);    

            //Debug.Log("Brand Damage Activated, Health is " +target.Health.ToString()); 
        }
        
        
    }
}
