using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyRevenge : CreatureEffect
{
    public int buffCooldown = 1;
   List<CreatureLogic> heroAllies = new List<CreatureLogic>();
    

    public CoyRevenge(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {
         heroAllies = owner.AllyList();
    }

   public override void RegisterEventEffect()
    {
        
        //heroAllies = owner.AllyList();
        foreach(CreatureLogic cl in heroAllies)
        {
            cl.e_IsAttacked += UseEffect;      
            //cl.e_ThisCreatureDies += UseEffect;
        }    
           
          
           //creature.e_IsAttacked += UseEffect;
    }

    public override void UnRegisterEventEffect()
    {       
     
        //heroAllies = owner.AllyList();
        foreach(CreatureLogic cl in heroAllies)
        {
            cl.e_IsAttacked -= UseEffect;              
            
        }    
            

            //creature.e_IsAttacked -= UseEffect;   
    }

    public override void UseEffect(CreatureLogic target)
    {
        if (ChanceOK(creature.chance))
        {
            if (target.owner == owner)
            {
                ShowAbility();
                AddBuff(target, "Retaliate",buffCooldown);
            }
            base.UseEffect();
        }

    }

    

    
}
