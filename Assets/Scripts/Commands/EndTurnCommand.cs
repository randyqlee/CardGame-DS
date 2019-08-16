using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EndTurnCommand : Command 
{
    

    public EndTurnCommand()
    {
           
    }

    public override void StartCommandExecution()
    {
         
        new ShowMessageCommand("End Turn!", GlobalSettings.Instance.MessageTime).AddToQueue();

         TurnManager.Instance.EndTurn();  

         CommandExecutionComplete();
    }
}
