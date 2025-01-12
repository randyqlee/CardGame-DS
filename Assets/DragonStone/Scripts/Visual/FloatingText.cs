﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

	public Animator animator;
	//seconds
	public float clipDuration = 0.1f;

	// Use this for initialization
	void Start () {
		AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
		Destroy(gameObject, clipDuration * clipInfo[0].clip.length);
		
	
		//Destroy(gameObject, 3f * clipInfo[0].clip.length);
		//Debug.Log ("Instantiate Floating text");
	}

	public void SetText (string text)
	{
		animator.GetComponent<Text>().text = text;

	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy()
	{
		Command.CommandExecutionComplete();

	}
}
