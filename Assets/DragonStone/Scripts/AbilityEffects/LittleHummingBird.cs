using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleHummingBird : CreatureEffect
{
    public int maxNumberOfAllies = 1;
    public int healValue = 5;

    public LittleHummingBird(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        if(CanUseAbility())
        {   
            if(ChanceOK(creature.chance))
            {         
                List<CreatureLogic> sortedList = owner.SortAllyListByHealth();
                if (sortedList.Count > 1)
                {
                    ShowAbility();
                    int count = 0;
                    for (int i = 0; i<sortedList.Count && count < maxNumberOfAllies; i++)
                    {
                        if (sortedList[i] != creature)
                        {
                            sortedList[i].Heal(healValue);
                            count++;
                            RemoveRandomDebuff(sortedList[i]);
                        }
                    }
                }
            }
            base.UseEffect();
        }
    }
}