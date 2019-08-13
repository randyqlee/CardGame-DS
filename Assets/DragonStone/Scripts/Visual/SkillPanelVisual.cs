using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillPanelVisual : MonoBehaviour {

	public GameObject abilities;

	public GameObject portraitWSkills;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Display()
	{
		gameObject.SetActive(true);
		Sequence s = DOTween.Sequence();
		s.Append(gameObject.transform.DOScale(1f,0.5f));
		s.OnComplete(() =>
		{
			Command.CommandExecutionComplete();
		});



	}
}
