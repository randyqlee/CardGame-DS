using UnityEngine;
using System.Collections;

public class DamageOpponentBattlecry : CreatureEffect
{
    public DamageOpponentBattlecry(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}

    // BATTLECRY
    public override void WhenACreatureIsPlayed()
    {
        //new DealDamageCommand(owner.otherPlayer.PlayerID, creatureEffectCooldown, owner.otherPlayer.Health - creatureEffectCooldown).AddToQueue();
        owner.otherPlayer.Health -= creatureEffectCooldown;
    }
}
