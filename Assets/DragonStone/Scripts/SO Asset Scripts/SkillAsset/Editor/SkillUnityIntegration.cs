using UnityEngine;
using UnityEditor;

static class SkillUnityIntegration {

	[MenuItem("CreateSO/SkillAsset")]
	public static void CreateYourScriptableObject() {
		ScriptableObjectUtility2.CreateAsset<SkillAsset>();
	}

}
