using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonlightGrace : CreatureEffect
{
    public int buffCooldown = 3;

    public int otherCooldown = 1;
    // Start is called before the first frame update
    public MoonlightGrace(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
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
        

        if(creatureEffectCooldown <= 0)
        {
            ShowAbility();
            
            if(creature.isPrimaryForm)
            {
                foreach(CreatureLogic cl in owner.AllyList())
                {
                    if (!cl.isDead)
                    {
                        cl.RemoveDeBuffsAll();
                        AddBuff(cl, "Immunity", otherCooldown);

                    }
                }

                AddBuff(creature, "Armor", buffCooldown);

                creature.isPrimaryForm = false;
                new CreatureTransformCommand (creature.UniqueCreatureID, creature.isPrimaryForm).AddToQueue();
            }

            else
            {
                foreach(CreatureLogic cl in owner.EnemyList())
                {
                    if (!cl.isDead)
                    {
                        cl.RemoveBuffsAll();
                        AddBuff(cl, "AntiBuff", otherCooldown);

                    }
                }

                AddBuff(creature, "IncreaseAttack", buffCooldown);

                creature.isPrimaryForm = true;
                new CreatureTransformCommand (creature.UniqueCreatureID, creature.isPrimaryForm).AddToQueue();
                
            }

            base.UseEffect();
        }


    }


}
