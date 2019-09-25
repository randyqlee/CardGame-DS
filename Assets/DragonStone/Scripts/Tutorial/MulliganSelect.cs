using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MulliganSelect : MonoBehaviour {

	MulliganScreen ms;

	public bool isSelected;

	public GameObject selectedIndicator;

	void Awake()
	{
		ms = gameObject.GetComponentInParent<MulliganScreen>();
		isSelected = false;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown()
	{
		ms.SelectEquipCreatures(gameObject.GetComponentInParent<OneCreatureManager>().gameObject, isSelected);
		isSelected = !isSelected;

		if (isSelected)
			selectedIndicator.SetActive(true);
		else
			selectedIndicator.SetActive(false);

	}
}
