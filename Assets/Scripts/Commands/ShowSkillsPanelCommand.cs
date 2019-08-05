using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowSkillsPanelCommand : Command {


	Player p;
	public ShowSkillsPanelCommand (Player p)
	{
		this.p = p;

	}

	public override void StartCommandExecution()
	{
		p.PArea.skillPanel.gameObject.SetActive(true);
        //p.PArea.skillPanel.gameObject.transform.DOScale(1f,0.5f);
		p.PArea.skillPanel.Display();
		
	}



}
