using UnityEngine;
using UnityEditor;

static class CardUnityIntegration 
{

	[MenuItem("CreateSO/CardAsset")]
	public static void CreateYourScriptableObject() {
		ScriptableObjectUtility2.CreateAsset<CardAsset>();
	}

}
