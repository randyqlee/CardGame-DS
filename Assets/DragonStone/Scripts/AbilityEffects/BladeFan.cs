using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class BladeFan : CreatureEffect 
{
    public int buffCount = 3;

    public BladeFan(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       creature.e_AfterAttacking += UseEffect;        
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_AfterAttacking += UseEffect;            
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {   
            if(ChanceOK(creature.chance))
            {
                ShowAbility();


                var buffList = new List<BuffEffect>();

                foreach (BuffEffect be in target.buffEffects)
                {
                    if(be.isBuff)
                    {
                        buffList.Add(be);
                    }
                }

                int buffRemoved = 0;

                if (buffList != null)
                {
                    foreach(BuffEffect be in buffList)
                    {
                        target.RemoveBuff(be);
                        buffRemoved++;
                    }
                }


                if(buffRemoved > buffCount)
                {
                    GainExtraTurn();
                }

            }
            
            base.UseEffect();
        }
    }

    public void GainExtraTurn()
    {
        creature.AttacksLeftThisTurn++;        
    }
}