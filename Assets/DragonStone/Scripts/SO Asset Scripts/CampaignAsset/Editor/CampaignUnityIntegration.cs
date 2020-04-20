using UnityEngine;
using UnityEditor;

static class CampaignUnityIntegration {

	//[MenuItem("Assets/Create/CampaignAsset")]
	public static void CreateYourScriptableObject() {
		ScriptableObjectUtility2.CreateAsset<CampaignAsset>();
	}

}
