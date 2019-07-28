using UnityEngine;
using System.Collections;

public class PlayerTurnMaker : TurnMaker 
{
    public override void OnTurnStart()
    {
        base.OnTurnStart();
        // dispay a message that it is player`s turn
        
        //DS - transfer to End Turn
        //new ShowMessageCommand("Your Turn!", GlobalSettings.Instance.MessageTime).AddToQueue();
        
        //DS
        //p.DrawACard();
    }
}
