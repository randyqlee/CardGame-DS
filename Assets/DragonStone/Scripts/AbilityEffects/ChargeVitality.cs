using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ChargeVitality : CreatureEffect {

    public int healAmount = 15;
    

    public ChargeVitality(Player owner, CreatureLogic creature, int creatureEffectCooldown): base(owner, creature, creatureEffectCooldown)
    {}


   public override void RegisterEventEffect()
    {
       creature.e_PreAttackEvent += IncreaseAttackCount;
       //creature.e_CreatureOnTurnStart += IncreaseAttackCount;
       creature.e_SecondAttack += UseEffect;      

       
    }

    public override void UnRegisterEventEffect()
    {
         creature.e_PreAttackEvent -= IncreaseAttackCount;
         //creature.e_CreatureOnTurnStart -= IncreaseAttackCount;     
         creature.e_SecondAttack -= UseEffect;      
    }

    public override void CauseEventEffect()
    {
       
    }

    //Attack Twice
    public override void UseEffect(CreatureLogic target)
    {        
        
        if(remainingCooldown<=0)
        {
            
            base.ShowAbility();
            SecondAttack(target);
            //base.UseEffect();          
           
            
        }        
        
    }

    void IncreaseAttackCount()
    {
       
        if(remainingCooldown<=0)
        {
            creature.AttacksLeftThisTurn++;
            
            SecondHealEffect();     
            
        }                             
    }

    void SecondAttack(CreatureLogic target)
    {               
        if(remainingCooldown<=0)
        {
            //creature.AttacksLeftThisTurn++; 
          
          //hasUsedEffect = true;   
          base.UseEffect();           
          if(!target.isDead)          
          creature.AttackCreature(target);

          //DS: Force for now  
                                 
         
           
        }
       
        
         
    }

    //Heal Effect Start of Turn
    public void SecondHealEffect()
    {
        
        

        //initialize
        creature.owner.AllyList();
        List<CreatureLogic> uniqueAllies = new List<CreatureLogic>();        
        
        //Get list of unique allies
        foreach(CreatureLogic cl in creature.owner.allies)
        {
            if(cl != creature)
            uniqueAllies.Add(cl);
        }
        
        //heal a random ally
        if(uniqueAllies.Count > 0)
        {
            CreatureLogic randAlly = uniqueAllies[Random.Range(0, uniqueAllies.Count)];

            ShowAbility(creature);
            creature.Heal(healAmount);              
            randAlly.Heal(healAmount);
            
        }else{
            ShowAbility(creature);
            creature.Heal(healAmount);    
        }     


        creature.owner.allies.Clear();
    }
    
}
