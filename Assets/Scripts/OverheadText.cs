using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheadText : MonoBehaviour {

	public GameObject text;

	void Awake()
	{
//		GetComponentInParent<HeroManager>().e_PopupMSG += FloatingText;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FloatingText (string message)
	{
		
		//StartCoroutine (DelayMessage(message));
		//DelayMessage(message);

		Debug.Log("calling Floatingtext " +message);

		FloatingText popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");
		FloatingText instance = Instantiate(popupText);
		//instance.transform.localScale = new Vector3 (1,1,1);
		//instance.transform.
		instance.transform.SetParent (transform,false);
		//instance.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
		//instance.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
		
		instance.SetText(message);
	}

	IEnumerator DelayMessage(string message)
	{
/*
		if (GetComponentInParent<HeroManager>().GetComponentInChildren<OverheadText>().GetComponentInChildren<FloatingText>() != null)
		{
			yield return new WaitForSeconds (0.5f);
		}

		FloatingText popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");
		FloatingText instance = Instantiate(popupText);
		instance.transform.SetParent (transform, false);
		instance.SetText(message);		
*/		
yield return null;
	}

	public void SetText (string message)
	{
		text.GetComponent<Text>().text = message;
		StartCoroutine (PopupText());

	}

	void ShowText ()
	{
		text.gameObject.SetActive(true);
	}

	void HideText ()
	{
		text.gameObject.SetActive(false);
	}

	public IEnumerator PopupText ()
	{
		
		ShowText();
		yield return new WaitForSeconds (3f);
		HideText();

		yield return null;

	}





}
