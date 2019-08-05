using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

	private FloatingText popupText;

	//private GameObject parent;

	public void Initialize()
	{
		//parent = GameObject.Find ("Canvas");
		popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");
	}

	public void CreateFloatingText(string text, Transform location)
	{
		FloatingText instance = Instantiate(popupText);
	//	instance.transform.SetParent(canvas.transform, false);
		instance.SetText(text);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
