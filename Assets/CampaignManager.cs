using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CampaignInfo
{
    public string name = "Campaign Name";
    public int difficulty = 1;
    public float reward = 100;

    public int playerSlots;
    public List<CardAsset> enemyList;

    public string campaignSceneName = "BattleScene_Campaign";
    public bool isEquipEnabled = false;
}


public class CampaignManager : MonoBehaviour
{

    public static CampaignManager Instance;

    public GameObject sceneReloader;

    public List<CampaignInfo> campaignList;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }


    public void StartCampaign(string campaignName)
    {

        foreach (CampaignInfo campaign in campaignList)
        {
            if(campaign.name == campaignName)
            {
                LoadCampaign(campaign);
                break;

            }
        }
    }

    public void LoadCampaign(CampaignInfo campaign)
    {

        BattleStartInfo.EnemyDeck = new DeckInfo(campaign.enemyList);
        sceneReloader.GetComponent<SceneReloader>().LoadScene("BattleScene_Campaign");


    }


}
