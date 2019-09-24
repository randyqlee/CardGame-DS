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
         //DS
        //new ShowMessageCommand("End Turn!", GlobalSettings.Instance.MessageTime).AddToQueue();

         TurnManager.Instance.EndTurn();  
 
         //CommandExecutionComplete();

        Sequence s = DOTween.Sequence();
        s.PrependInterval(2f);
        s.OnComplete(()=>
        {
            new ShowMessageCommand("End Turn!", GlobalSettings.Instance.MessageTime).AddToQueue();
            //TurnManager.Instance.EndTurn();  
            Command.CommandExecutionComplete();
        }
        
        );
            
        
    }
}
