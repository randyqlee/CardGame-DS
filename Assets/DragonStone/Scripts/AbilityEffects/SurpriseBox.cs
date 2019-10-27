using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseBox : CreatureEffect
{
    public int buffCooldown = 2;
    public int damage = 6;

    public SurpriseBox(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       creature.e_PreAttackEvent += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= UseEffect;          
    }

    public override void UseEffect(CreatureLogic target)
    {
        if (CanUseAbility())
        {
            ShowAbility();
            //DealDamageEffect(target,damage);
            //creature.SplashAttackDamage(target,damage);
            foreach(CreatureLogic cl in owner.EnemyList())
            {               
               if(!cl.isDead)
               {
                   DealDamageEffect(cl, damage);
                   if(!cl.isDead) 
                   {
                       DebuffList randomDebuff = GetRandomEnum<DebuffList>();
                       AddBuff(cl, randomDebuff.ToString(), buffCooldown);
                   }                   
               }              
                
            }   

            base.UseEffect();

        }
    }
}
