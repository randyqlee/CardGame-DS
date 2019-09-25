using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour {

	public List<string> commandQueue;

	public static QueueManager Instance;
	// Use this for initialization

	void Awake()
	{
		Instance = this;
	}
	void Start () {

		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
