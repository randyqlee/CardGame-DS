using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ChargeVitality : CreatureEffect

{
    public int healValue = 15;

    public int maxNumberOfAllies = 1;

    public ChargeVitality(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }
    public override void RegisterEventEffect()
    {
       creature.e_PreAttackEvent += IncreaseAttackCount;
       creature.e_SecondAttack += UseEffect;       
 
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= IncreaseAttackCount;
        creature.e_SecondAttack -= UseEffect;   
         
    }

    void IncreaseAttackCount(CreatureLogic target)
    {
       
        if(CanUseAbility())
        {
            ShowAbility();
            creature.AttacksLeftThisTurn++;

            creature.Heal(healValue);

            List<CreatureLogic> sortedList = owner.SortAllyListByHealth();
            if (sortedList.Count > 1)
            {
                //ShowAbility();
                int count = 0;
                for (int i = 0; i<sortedList.Count && count < maxNumberOfAllies; i++)
                {
                    if (sortedList[i] != creature)
                    {
                        sortedList[i].Heal(healValue);                        
                        count++;
                    }
                }
            }          
        }                             
    }

    public override void UseEffect(CreatureLogic target)
    {
        if (CanUseAbility())
        {
            //ShowAbility();
            if(!target.isDead)          
                creature.AttackCreature(target);
            base.UseEffect();  

        }

    }
}
