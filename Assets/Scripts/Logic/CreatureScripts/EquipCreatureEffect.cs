using UnityEngine;
using System.Collections;

public class EquipCreatureEffect : CreatureEffect
{
    public EquipCreatureEffect(Player owner, CreatureLogic creature) : base(owner, creature)
    {

        this.owner = owner;
        this.creature = creature;
    }

    // BATTLECRY
    public override void WhenACreatureIsPlayed()
    {
        

        foreach (CreatureLogic cl in owner.AllyList())
        {
            if(!cl.isEquip)
            {
                new ShowMessageCommand("EQUIP: +1 Atk", GlobalSettings.Instance.MessageTime).AddToQueue();
                cl.Attack += 1;
            
                
                new UpdateAttackCommand(cl.ID, cl.Attack).AddToQueue();


                new ShowMessageCommand("EQUIP: Torrent", GlobalSettings.Instance.MessageTime).AddToQueue();
                CreatureEffect effect = System.Activator.CreateInstance(System.Type.GetType("Torrent"), new System.Object[]{owner, cl, 0}) as CreatureEffect;
                    effect.RegisterCooldown();
                    effect.RegisterEventEffect();

                    cl.creatureEffects.Add(effect);


               
            }

        }


        
    }
}
