using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationQuit : MonoBehaviour {

	public string previousScene;

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (previousScene == null || previousScene == "")
				Application.Quit();
			else
			{
				StartCoroutine(SceneSwitch());
				
			}


		}
	}

	public void Back()
	{

		StartCoroutine(SceneSwitch());
		
		//SceneManager.LoadScene(previousScene);

	}

	IEnumerator SceneSwitch()
	{
		SceneManager.LoadScene(previousScene, LoadSceneMode.Additive);
		yield return null;
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

	}
}
