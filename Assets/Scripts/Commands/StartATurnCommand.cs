using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StartATurnCommand : Command {

    private Player p;

    public StartATurnCommand(Player p)
    {
        this.p = p;
    }

    public override void StartCommandExecution()
    {
        
        //new ShowMessageCommand("Your Turn!", GlobalSettings.Instance.MessageTime).AddToQueue();
        
        //TurnManager.Instance.whoseTurn = p;

        //CommandExecutionComplete();

        Sequence s = DOTween.Sequence();
        //s.AppendInterval(2f);
        s.OnComplete(()=>
        {
            new ShowMessageCommand("Your Turn!", GlobalSettings.Instance.MessageTime).AddToQueue();
            TurnManager.Instance.whoseTurn = p; 
            Command.CommandExecutionComplete();
        }
        
        );
        
    }
}
