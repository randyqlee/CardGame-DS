using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnluckySeven : CreatureEffect
{
    public int buffCooldown = 1;

    public UnluckySeven(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if(CanUseAbility())
        {
            if(ChanceOK(creature.chance))
            {
                ShowAbility();

                int i = Random.Range(0,2);
                string randomDebuff;

                if (i == 1)
                    randomDebuff = "DecreasedAttack";
                else
                    randomDebuff = "Silence";

                foreach(CreatureLogic cl in owner.EnemyList())
                {
                    AddBuff(cl, randomDebuff, buffCooldown);
                }  
            }

            base.UseEffect();
        }
    }
}
