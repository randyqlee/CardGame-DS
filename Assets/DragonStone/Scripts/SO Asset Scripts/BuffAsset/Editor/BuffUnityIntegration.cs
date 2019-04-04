using UnityEngine;
using UnityEditor;

static class BuffUnityIntegration {

	[MenuItem("Assets/Create/BuffAsset")]
	public static void CreateYourScriptableObject() {
		ScriptableObjectUtility2.CreateAsset<BuffAsset>();
	}

}
