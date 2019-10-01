using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resurrect : BuffEffect {

    //int attackModifier = 5;
    bool REflag = false;
    
	
    public Resurrect(CreatureLogic source, CreatureLogic target, int buffCooldown) : base (source, target, buffCooldown)
    {
        buffIcon = Resources.Load<Sprite>("BuffIcons/Resurrect");
        isBuff = true;}

    public override void CauseBuffEffect()
    {
        
        target.e_ThisCreatureDies += ResurrectEffect;        
       
        //target.isDead = false;
    }

    public override void UndoBuffEffect()
    {
         //target.e_CreatureOnTurnEnd -= ResurrectEffect;
         //target.hasResurrect = false;
    }


    public void ResurrectEffect(CreatureLogic target)
    {
        target.Revive();   
        
    //    if(target.isDead)
    //    {
    //        new DelayCommand(1f).AddToQueue();
            //New SCRIPT
          
    //        target.isDead = false;
    //        REflag = true;           

            
        /* Replace with Ability SO

            if (target.ca.abilityEffect != null)
            {
                foreach (AbilityEffect ae in target.ca.abilityEffect)
                {
                    if (ae.CreatureScriptName != null && ae.CreatureScriptName != "")
                    {
                        target.effect = System.Activator.CreateInstance(System.Type.GetType(ae.CreatureScriptName), new System.Object[]{target.owner, target, ae.coolDown}) as CreatureEffect;
                        target.effect.RegisterCooldown();
                        target.effect.RegisterEventEffect();
                        target.creatureEffects.Add(target.effect);
                    }
                }
            }

        */ 
        //    if (target.ca.Abilities != null)
        //    {
        //        foreach (AbilityAsset ae in target.ca.Abilities)
        //        {
        //            if (ae.abilityEffect != null && ae.abilityEffect != "")
        //            {
        //                target.effect = System.Activator.CreateInstance(System.Type.GetType(ae.abilityEffect), new System.Object[]{target.owner, target, ae.abilityCoolDown}) as CreatureEffect;
        //                target.effect.RegisterCooldown();
        //                target.effect.RegisterEventEffect();
        //               target.creatureEffects.Add(target.effect);
        //            }
        //        }
        //    }                                        
                             
            target.Health = target.MaxHealth;
            target.Attack = target.Attack;

            new UpdateAttackCommand(target.UniqueCreatureID, target.Attack).AddToQueue();
            new UpdateHealthCommand(target.UniqueCreatureID, target.Health).AddToQueue();

            
            //new CreatureResurrectCommand(target.UniqueCreatureID, target.owner).AddToQueue();  

            //Remove all buffs/debuffs        
            //target.RemoveAllBuffs(); 
            
             this.buffCooldown = 0;
             target.e_ThisCreatureDies -= ResurrectEffect;
        //}           
               
    }//ResurrectEffect  

    
}
