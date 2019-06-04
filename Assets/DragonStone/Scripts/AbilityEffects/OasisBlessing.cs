using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class OasisBlessing : CreatureEffect {

    List<CreatureLogic> allies = new List<CreatureLogic>();
    public int buffCooldown = 3;

	public OasisBlessing(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
        creature.e_CreatureOnTurnStart += UseEffect;
    }

    public override void UnRegisterEventEffect()
    {
        creature.e_CreatureOnTurnStart -= UseEffect;
    }

    public override void UseEffect()
    {
        if(remainingCooldown <=0)
        {
            creature.Heal(1);        
            foreach(CreatureLogic ce in creature.owner.AllyList())
            {
            if(!ce.isDead)
            allies.Add(ce);
            }      

            CreatureLogic randomAlly = allies[Random.Range(0,allies.Count)];

            Debug.Log("Random Ally: " +randomAlly.UniqueCreatureID);
            
            //AddBuff(randomAlly, "IncreaseAttack",buffCooldown);
            //AddBuff(randomAlly, "Recovery",buffCooldown);
            //AddBuff(randomAlly, "Retaliate",buffCooldown);
             //AddBuff(randomAlly, "Immunity",buffCooldown);
            //AddBuff(randomAlly, "Invincibility",buffCooldown);
            //AddBuff(randomAlly, "Resurrect",buffCooldown);
            //AddBuff(randomAlly, "Taunt",buffCooldown);
        }
    }
}
