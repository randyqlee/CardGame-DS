using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnDelay : MonoBehaviour {



	public static EndTurnDelay Instance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Delay()
	{
		StartCoroutine(DelayCoroutine());

	}

	IEnumerator DelayCoroutine()
	{
		yield return new WaitForSeconds(10f);
		new ShowMessageCommand("End Turn!", GlobalSettings.Instance.MessageTime).AddToQueue();

         TurnManager.Instance.EndTurn();  

         


	}
}
