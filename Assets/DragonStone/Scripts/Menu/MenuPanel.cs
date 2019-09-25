using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour {

	public List<GameObject> buttons;

	public float btnWidth;
	public float btnHeight;

	public float scale = 1.5f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ActivateButton (GameObject go)
	{
		foreach (GameObject buttons in buttons)
		{
			if (buttons == go)
				buttons.GetComponent<RectTransform>().sizeDelta = new Vector2 (btnWidth * scale, btnHeight * scale);
			else 
				buttons.GetComponent<RectTransform>().sizeDelta = new Vector2 (btnWidth, btnHeight);			
		}
	}
}
