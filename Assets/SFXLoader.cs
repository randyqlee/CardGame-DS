using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXLoader : MonoBehaviour {

	public GameObject SFX_Prefab;


	// Use this for initialization

	void Awake()
	{
		
	}

	void Start () {

		Instantiate (SFX_Prefab,gameObject.transform);
		
		
	}
	
	// Update is called once per frame
	void Update () {

		
		
	}
}
