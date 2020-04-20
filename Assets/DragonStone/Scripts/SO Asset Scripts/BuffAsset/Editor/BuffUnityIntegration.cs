using UnityEngine;
using UnityEditor;

static class BuffUnityIntegration {

	[MenuItem("CreateSO/BuffAsset")]
	public static void CreateYourScriptableObject() {
		ScriptableObjectUtility2.CreateAsset<BuffAsset>();
	}

}
