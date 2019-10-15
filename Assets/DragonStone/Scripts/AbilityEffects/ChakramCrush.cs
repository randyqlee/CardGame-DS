using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChakramCrush : CreatureEffect
{

    public int buffCooldown = 1;
    // Start is called before the first frame update
    public ChakramCrush(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
       //creature.e_CreatureOnTurnStart += UseEffect;      
       //creature.e_PreAttackEvent += UseEffect; 
       creature.e_AfterAttacking += UseEffect; 
      
    }

    public override void UnRegisterEventEffect()
    {
         //creature.e_CreatureOnTurnStart -= UseEffect;  
         //creature.e_PreAttackEvent -= UseEffect;
         creature.e_AfterAttacking -= UseEffect; 
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(creatureEffectCooldown <= 0)
        {
            ShowAbility();
            creature.SplashAttackDamage(target, creature.AttackDamage);
            AddBuff(target, "Stun", buffCooldown);
           

            base.UseEffect();
        }
    }


}
