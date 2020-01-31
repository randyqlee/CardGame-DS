using UnityEngine;
using System.Collections;

public class BiteOwner : CreatureEffect
{  
    public BiteOwner(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}

    public override void RegisterEventEffect()
    {
        owner.EndTurnEvent += CauseEventEffect;
        //owner.otherPlayer.EndTurnEvent += CauseEventEffect;
        //Debug.Log("Registered bite effect!!!!");
    }

    public override void UnRegisterEventEffect()
    {
        owner.EndTurnEvent -= CauseEventEffect;
    }

    public override void CauseEventEffect()
    {
        //Debug.Log("InCauseEffect: owner: "+ owner + " creatureEffectCooldown: "+ creatureEffectCooldown);
        //new DealDamageCommand(owner.PlayerID, creatureEffectCooldown, owner.Health - creatureEffectCooldown).AddToQueue();
//        owner.Health -= creatureEffectCooldown;
    }


}
