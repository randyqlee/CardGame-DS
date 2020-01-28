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
        s.AppendInterval(1f);     
        s.OnComplete(()=>
        {

            if(p.PArea.owner == AreaPosition.Top)
                new ShowMessageCommand("Enemy Turn!", GlobalSettings.Instance.MessageTime).AddToQueue();
            else
                new ShowMessageCommand("Your Turn!", GlobalSettings.Instance.MessageTime).AddToQueue();

            TurnManager.Instance.whoseTurn = p; 
            Command.CommandExecutionComplete();
        }
        
        );
        
    }
}
