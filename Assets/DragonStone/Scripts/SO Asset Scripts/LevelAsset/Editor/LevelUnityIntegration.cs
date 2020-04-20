using UnityEngine;
using UnityEditor;

static class LevelUnityIntegration {

	//[MenuItem("Assets/Create/LevelAsset")]
	public static void CreateYourScriptableObject() {
		ScriptableObjectUtility2.CreateAsset<LevelAsset>();
	}

}
