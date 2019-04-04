using UnityEngine;
using UnityEditor;

static class AbilityUnityIntegration {

	[MenuItem("Assets/Create/AbilityAsset")]
	public static void CreateYourScriptableObject() {
		ScriptableObjectUtility2.CreateAsset<AbilityAsset>();
	}

}
