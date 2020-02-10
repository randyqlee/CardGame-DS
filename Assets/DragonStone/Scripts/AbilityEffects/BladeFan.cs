using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class BladeFan : CreatureEffect 
{
    public int buffCount = 3;

    public bool stayActive = false;

    public BladeFan(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       creature.e_AfterAttacking += UseEffect;
       creature.e_CreatureOnTurnEnd += GainExtraTurn;        
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_AfterAttacking -= UseEffect;
        creature.e_CreatureOnTurnEnd -= GainExtraTurn;                
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {   
            if(ChanceOK(creature.chance))
            {
                ShowAbility();

                stayActive = false;

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
                        BuffSystem.Instance.RemoveBuff(target,be);
                        buffRemoved++;
                    }
                }


                if(buffRemoved > buffCount)
                {
                    stayActive = true;
                }

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