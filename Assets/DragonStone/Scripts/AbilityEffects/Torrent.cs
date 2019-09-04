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
        //creature.e_CreatureOnTurnStart += UseEffect;  
        creature.e_PreAttackEvent += UseEffect;    
    }

    public override void UnRegisterEventEffect()
    {        
        //creature.e_CreatureOnTurnStart -= UseEffect;
        creature.e_PreAttackEvent -= UseEffect;
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
                base.ShowAbility();
                AddBuff(creature, "IncreaseAttack", 2);
                  AddBuff(creature, "Immunity", 2);
                base.UseEffect();      
                    
            }            
           
           
        }    




       
    }
}
