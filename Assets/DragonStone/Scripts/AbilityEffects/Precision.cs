using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Precision : CreatureEffect
{
    public int buffCooldown = 1;
    public Precision(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

    public override void RegisterEventEffect()
    {
       //creature.e_CreatureOnTurnStart += UseEffect;      
       creature.e_PreAttackEvent += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {
         //creature.e_CreatureOnTurnStart -= UseEffect;  
         creature.e_PreAttackEvent -= UseEffect;          
    }

    public override void UseEffect(CreatureLogic target)
    {
        //BattleCry: Chance to give an ally armor and inflict Brand to the enemy target.
        if(Random.Range(0,100)<=creature.chance)
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


        base.UseEffect();


    }


}
