using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FloatingTweenText : MonoBehaviour {

	public Text text;
	public Vector3 endValue;
	public float tweenDuration;

	public float delayBeforeDestroy;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetText (string message)
	{
		text.text = message;
		
	}

	public void Move ()
	{



		transform.DOMove(new Vector3(transform.position.x + endValue.x, transform.position.y + endValue.y, transform.position.z + endValue.z), tweenDuration, false);
		//Sequence s = DOTween.Sequence();
                //s.AppendInterval(delay);
                //s.OnComplete(Command.CommandExecutionComplete);
				//s.OnComplete(Destroy(gameObject));

		Destroy(gameObject,delayBeforeDestroy);
		
	}

	void OnDestroy()
	{
		Command.CommandExecutionComplete();
	}

}
