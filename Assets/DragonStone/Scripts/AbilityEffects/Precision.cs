using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Precision : CreatureEffect
{
    public int buffCooldown = 1;

    bool effectChance = false;
    public Precision(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
       creature.e_PreAttackEvent += CheckChance;      
       //creature.e_AfterAttacking += UseEffect;  
       creature.e_PreAttackEvent += UseEffect;
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_PreAttackEvent -= CheckChance;  
        //creature.e_AfterAttacking -= UseEffect;
        creature.e_PreAttackEvent -= UseEffect;          
    }

    public void CheckChance(CreatureLogic target)
    {
        if (creatureEffectCooldown <= 0)
        {
            effectChance = false;
            if(ChanceOK(creature.chance))
            {

                ShowAbility();
                
                effectChance = true;
                creature.CriticalChance += 1;
                

            }
        }

    }
    public override void UseEffect(CreatureLogic target)
    {
        if (creatureEffectCooldown <= 0)
        {
            /*
            if(ChanceOK(creature.chance))
            {
                int i = Random.Range(0,owner.table.CreaturesOnTable.Count);
                while (owner.table.CreaturesOnTable[i].isDead && owner.table.CreaturesOnTable[i] != creature)
                {
                    i = Random.Range(0,owner.table.CreaturesOnTable.Count);
                }
                CreatureLogic ally = owner.table.CreaturesOnTable[i];
                AddBuff(ally, "Armor", buffCooldown);


                int j = Random.Range(0,owner.otherPlayer.table.CreaturesOnTable.Count);
                while (owner.otherPlayer.table.CreaturesOnTable[j].isDead)
                {
                    j = Random.Range(0,owner.otherPlayer.table.CreaturesOnTable.Count);
                }
                CreatureLogic enemy = owner.otherPlayer.table.CreaturesOnTable[j];
                AddBuff(enemy, "Brand", buffCooldown);

            }
            */

            if(effectChance)
            {
                AddBuff(target, "Unlucky", buffCooldown);
                creature.CriticalChance -= 1;
            }

            base.UseEffect();
        }
    }


}
