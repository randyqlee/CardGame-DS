using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilate : CreatureEffect{
    public int buffCooldown = 1;
    public Ventilate(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       creature.e_PreAttackEvent += UseEffect;        
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent += UseEffect;            
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {   
            if(ChanceOK(creature.chance))
            {
                ShowAbility();
                CreatureLogic cl = owner.GetRandomAlly(creature);
                if (cl != null)
                {
                    foreach(CreatureEffect ce in cl.creatureEffects)
                    {
                        ce.RefreshCreatureEffectCooldown();
                    }
                    AddBuff(cl, "Lucky",buffCooldown);
                }
            }
            
            base.UseEffect();
        }
    }
}