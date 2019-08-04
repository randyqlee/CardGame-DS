using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheadText : MonoBehaviour {

	public GameObject text;
	public Vector3 location = new Vector3();

	void Awake()
	{
//		GetComponentInParent<HeroManager>().e_PopupMSG += FloatingText;
	}

	// Use this for initialization
	void Start () {

	

		
		
	}
	
	// Update is called once per frame
	void Update () {

		//FloatingText ("Text");


		
		
	}

	public void FloatingText (string message)
	{
		
		//StartCoroutine (DelayMessage(message));
		//DelayMessage(message);

		//Debug.Log("calling Floatingtext " +message);

//		FloatingText popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");
//		FloatingText instance = Instantiate(popupText);
//		instance.transform.SetParent (transform,false);
//		instance.SetText(message);


		GameObject go = Instantiate(GlobalSettings.Instance.FloatingTweenText_Prefab,gameObject.transform.position,Quaternion.identity);
		//go.transform.SetParent (transform,false);
		go.GetComponent<FloatingTweenText>().SetText(message);
		go.GetComponent<FloatingTweenText>().Move();

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
		yield return new WaitForSeconds (10f);
		HideText();

		yield return null;

	}




//Use for ShowSkillPreviewCommand
	public void ShowSkillPreview(CreatureEffect ce)
	{

		GameObject go = Instantiate(GlobalSettings.Instance.SkillCardPreview_Prefab,gameObject.transform.position,Quaternion.identity);
		//GameObject go = Instantiate(GlobalSettings.Instance.SkillCardPreview_Prefab,new Vector3(-6f, 0f, 0f),Quaternion.identity);
		//GameObject go = Instantiate(GlobalSettings.Instance.SkillCardPreview_Prefab,location,Quaternion.identity);

		go.GetComponent<SkillCardPreview>().SetupPreview(ce);
		go.GetComponent<SkillCardPreview>().Move();

	}	





}
