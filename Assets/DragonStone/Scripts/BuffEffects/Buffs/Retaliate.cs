using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retaliate : BuffEffect {

	
    public Retaliate(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Retaliate");
        isBuff = true;}

    public override void CauseBuffEffect()
    { 
        target.e_IsAttackedBy += RetaliateEffect;         
        //target.targetAttackDamage = target.Attack;
        //Debug.Log("Target's Attack Damage: " +target.targetAttackDamage);
    }

    public override void UndoBuffEffect()
    {
        target.e_IsAttackedBy -= RetaliateEffect;  
    }

    public void RetaliateEffect(CreatureLogic attacker)
    {
        new ShowBuffPreviewCommand(this, attacker.ID, this.GetType().Name+" Damage").AddToQueue();
          new SfxExplosionCommand(attacker.UniqueCreatureID).AddToQueue();

        new DealDamageCommand(attacker.ID, target.AttackDamage, healthAfter: attacker.TakeOtherDamageVisual(target.AttackDamage), armorAfter:attacker.TakeArmorDamageVisual(target.AttackDamage)).AddToQueue();

        attacker.TakeOtherDamage(target.AttackDamage);     
        

    }

    
    
    
}//DecreaseAttack Method
