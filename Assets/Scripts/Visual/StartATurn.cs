using System.Collections;
//using System.Collections.Generic;
using UnityEngine;



//DS - NOT USED

public class StartATurn : MonoBehaviour {

	public float delay = 0.5f;

	public static StartATurn Instance;

	void Awake()
		{
			Instance = this;
		}	

	IEnumerator startTurnCoroutine(GameObject obj)
		{
			Debug.Log("IENUmerator StartATurn");
			new ShowMessageCommand("Your Turn!", GlobalSettings.Instance.MessageTime).AddToQueue();
			
			yield return new WaitForSeconds (5f);

			GlobalSettings.Instance.EndTurnButton.interactable = true;
			Debug.Log("Interactable");
			// this command is completed instantly

			

		}

	public void StartMyCoroutine() {

		Debug.Log("startTurn StartATurn");
	GameObject obj = new GameObject();
	obj.AddComponent<StartATurn>().StartCoroutine(startTurnCoroutine(obj));
	}

}
