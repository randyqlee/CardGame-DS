using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetRevenge : CreatureEffect
{
    public int buffCooldown = 1;
    public int maxNumberOfAllies = 1;

    public SweetRevenge(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
                ShowAbility();
                foreach(CreatureLogic cl in owner.AllyList())
                {
                    cl.RemoveRandomDebuff();
                }
                List<CreatureLogic> sortedList = owner.SortAllyListByHealth();
                if (sortedList.Count > 1)
                {
                    ShowAbility();
                    int count = 0;
                    for (int i = 0; i<sortedList.Count && count < maxNumberOfAllies; i++)
                    {
                        if (sortedList[i] != creature)
                        {
                            AddBuff(sortedList[i], "Shield", buffCooldown);
                            count++;
                        }
                    }
                }              
            }
            
            base.UseEffect();
        }
    }
}