using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Torrent : CreatureEffect {

    public int healthLimit = 20;
    

    public Torrent(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {     
        creature.e_CreatureOnTurnStart += UseEffect;      
    }

    public override void UnRegisterEventEffect()
    {        
        creature.e_CreatureOnTurnStart -= UseEffect;
    }

    public override void CauseEventEffect()
    {
       
    }
    
    public override void UseEffect()
    {        
        if(remainingCooldown<=0)
        {
            if(creature.Health <= healthLimit)
            {
                ShowAbility();
                AddBuff(creature, "IncreaseAttack", 0);
                base.UseEffect();      
                    
            }            
           
           
        }    




       
    }
}
