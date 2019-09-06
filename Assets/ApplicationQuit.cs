using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationQuit : MonoBehaviour {

	public string previousScene;

	public GameObject exitScreen;

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
			if (exitScreen.activeSelf == true)
				Quit();

			else
			{


				if (previousScene == null || previousScene == "")
					exitScreen.SetActive(true);
				else
				{
					StartCoroutine(SceneSwitch());
					
				}
			}


		}
	}

	public void Quit()
	{
		     // save any game data here
     #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
     #else
         Application.Quit();
     #endif
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
