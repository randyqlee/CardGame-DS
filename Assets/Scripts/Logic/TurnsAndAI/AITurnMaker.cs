using UnityEngine;
using System.Collections;

//this class will take all decisions for AI. 

public class AITurnMaker: TurnMaker {

    public bool isPaused = false;

    public override void OnTurnStart()
    {
        base.OnTurnStart();
        // dispay a message that it is enemy`s turn
        //new ShowMessageCommand("AI`s Turn!", GlobalSettings.Instance.MessageTime).AddToQueue();
        //p.DrawACard();

        bool hasActiveCL = false;

        foreach (CreatureLogic cl in p.table.CreaturesOnTable)
        {
            if (cl.isActive)
                hasActiveCL = true;
        }

        if(!hasActiveCL)
            new EndTurnCommand().AddToQueue();


        else
            StartCoroutine(MakeAITurn());
    }

    // THE LOGIC FOR AI
    IEnumerator MakeAITurn()
    {
        do
        {
            yield return null;

        }
        while (isPaused);


        bool strategyAttackFirst = false;
        //if (Random.Range(0, 2) == 0)
            strategyAttackFirst = true;

        while (MakeOneAIMove(strategyAttackFirst))
        {
            //yield return null;
        }

        InsertDelay(1f);

        //DS
        //TurnManager.Instance.EndTurn();
        //new EndTurnCommand().AddToQueue();

        yield return null;
    }

    bool MakeOneAIMove(bool attackFirst)
    {
    //    if (Command.CardDrawPending())
    //      return true;
    //    else if (attackFirst)
    //        return AttackWithACreature() || PlayACardFromHand() || UseHeroPower();
    //    else 
    //        return PlayACardFromHand() || AttackWithACreature() || UseHeroPower();
        return AttackWithACreature();
    }

    bool PlayACardFromHand()
    {
        foreach (CardLogic c in p.hand.CardsInHand)
        {
            if (c.CanBePlayed)
            {
                if (c.ca.MaxHealth == 0)
                {
                    // code to play a spell from hand
                    // TODO: depending on the targeting options, select a random target.
                    if (c.ca.Targets == TargetingOptions.NoTarget)
                    {
                        p.PlayASpellFromHand(c, null);
                        InsertDelay(1.5f);
                        //Debug.Log("Card: " + c.ca.name + " can be played");
                        return true;
                    }                        
                }
                else
                {
                    // it is a creature card
                    p.PlayACreatureFromHand(c, 0);
                    InsertDelay(1.5f);
                    return true;
                }

            }
            //Debug.Log("Card: " + c.ca.name + " can NOT be played");
        }
        return false;
    }

    bool UseHeroPower()
    {
        if (p.ManaLeft >= 2 && !p.usedHeroPowerThisTurn)
        {
            // use HP
            p.UseHeroPower();
            InsertDelay(1.5f);
            //Debug.Log("AI used hero power");
            return true;
        }
        return false;
    }

    bool CanACreatureAttack()
    {
        bool canAttack = false;
        for (int i = 0; i < p.table.CreaturesOnTable.Count; i++)
        {
            if (!p.table.CreaturesOnTable[i].isDead && p.table.CreaturesOnTable[i].AttacksLeftThisTurn > 0)
                canAttack = true;
        }
        return canAttack;
    }
    
    bool AttackWithACreature()
    {
        //DS - use this for Round-based TurnManager
        if(CanACreatureAttack())
        {
            int i = Random.Range(0,p.table.CreaturesOnTable.Count);
            while (p.table.CreaturesOnTable[i].isDead || p.table.CreaturesOnTable[i].AttacksLeftThisTurn <= 0)
            {
                i = Random.Range(0,p.table.CreaturesOnTable.Count);
            }
            CreatureLogic cl = p.table.CreaturesOnTable[i];
            



            //foreach (CreatureLogic cl in p.table.CreaturesOnTable)
            //{
                if (cl.AttacksLeftThisTurn > 0)
                {
                    // attack a random target with a creature
                    if (p.otherPlayer.table.CreaturesOnTable.Count > 0)
                    {
                        int index = Random.Range(0, p.otherPlayer.table.CreaturesOnTable.Count);
                        while (p.otherPlayer.table.CreaturesOnTable[index].isDead){
                            index = Random.Range(0, p.otherPlayer.table.CreaturesOnTable.Count);
                        }
                        CreatureLogic targetCreature = p.otherPlayer.table.CreaturesOnTable[index];
                        cl.AttackCreatureWithID(targetCreature.UniqueCreatureID);
                    }                                    
                    InsertDelay(1f);
                    //return true;
                }
            //}
            return false;
        }
        
        else
        {
            TurnManager.Instance.EndTurn(); 
        
            return false;
        }

        //return false;
    }

    void InsertDelay(float delay)
    {
        //new DelayCommand(delay).AddToQueue();
    }

}
