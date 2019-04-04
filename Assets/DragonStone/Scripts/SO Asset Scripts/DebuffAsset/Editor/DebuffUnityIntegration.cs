using UnityEngine;
using UnityEditor;

static class DebuffUnityIntegration {

	[MenuItem("Assets/Create/DebuffAsset")]
	public static void CreateYourScriptableObject() {
		ScriptableObjectUtility2.CreateAsset<DebuffAsset>();
	}

}
