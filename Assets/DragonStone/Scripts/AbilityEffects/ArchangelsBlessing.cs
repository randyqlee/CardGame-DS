using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;


[System.Serializable]

public class ArchangelsBlessing : CreatureEffect 
{

    public int maxNumberOfAllies = 1;

    public ArchangelsBlessing(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
            List<CreatureLogic> sortedList = owner.SortAllyListByHealth();
            if (sortedList.Count > 1)
            {
                ShowAbility();
                int count = 0;
                for (int i = 0; i<sortedList.Count && count < maxNumberOfAllies; i++)
                {
                    if (sortedList[i] != creature)
                    {
                        sortedList[i].Heal(sortedList[i].MaxHealth- sortedList[i].Health);
                        count++;
                    }
                }
            }              
                     
            base.UseEffect();
        }
    }
}