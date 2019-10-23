using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eternity : CreatureEffect
{
    public int maxNumberOfAllies = 1;
    public int healValue = 8;

    public Eternity(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
        
    }

   public override void RegisterEventEffect()
    {
    
       TurnManager.Instance.e_EndOfRound += UseEffect;

       //creature.e_ThisCreatureDies += Revive;      
    }

    public override void UnRegisterEventEffect()
    {
        TurnManager.Instance.e_EndOfRound -= UseEffect;          
    }

    public override void UseEffect(CreatureLogic target)
    {
        if(CanUseAbility())
        {   
            ShowAbility();
            foreach(CreatureLogic cl in owner.AllyList())
            {
                if (cl != creature)
                {
                    cl.Heal(healValue);
                }
            }

            if (ChanceOK(creature.chance))
            {
                if (creature.isDead)
                {
                    new ReviveCommand(creature).AddToQueue();
                }

            }         

            base.UseEffect();
        }
    }

/*
    public void Revive (CreatureLogic target)
    {
        if (ChanceOK(creature.chance))
        {
            if(owner.AllyList() != null)
            {
                new ReviveCommand(creature).AddToQueue();
            }

        }      

    }
    */
}