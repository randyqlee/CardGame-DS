using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Torrent : CreatureEffect {

    public int buffCooldown = 1;
    
    public Torrent(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {     
        //creature.e_CreatureOnTurnStart += UseEffect;  
        creature.e_AfterAttacking += UseEffect;    
    }

    public override void UnRegisterEventEffect()
    {        
        //creature.e_CreatureOnTurnStart -= UseEffect;
        creature.e_AfterAttacking -= UseEffect;
    }

    public override void CauseEventEffect()
    {
       
    }
    
    public override void UseEffect(CreatureLogic target)
    {        
        if(remainingCooldown<=0)
        {
            ShowAbility();
            int damage = creature.MaxHealth - creature.Health;
            DealDamageEffect(target,damage);
            AddBuff(target, "Brand", buffCooldown);

            base.UseEffect();             
           
        }    




       
    }
}
