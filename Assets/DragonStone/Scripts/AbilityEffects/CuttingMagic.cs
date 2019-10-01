using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingMagic : CreatureEffect
{
    public int buffCooldown = 2;

    // Start is called before the first frame update
    public CuttingMagic(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
       creature.e_AfterAttacking += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
       creature.e_AfterAttacking -= UseEffect;           
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(creatureEffectCooldown <= 0)
        {
            ShowAbility();

            target.RemoveBuffsAll();

            foreach(CreatureLogic cl in owner.AllyList())
                {
                    AddBuff(cl, "Evasion", buffCooldown);
                }

            base.UseEffect();
        }
    }
}
