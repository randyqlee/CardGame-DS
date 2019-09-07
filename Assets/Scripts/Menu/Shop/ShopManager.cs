using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopManager : MonoBehaviour {

    public GameObject ScreenContent;
    public GameObject PackPrefab;
    public int PackPrice;
    public Transform PacksParent;
    public Transform InitialPackSpot;
    public float PosXRange = 4f;
    public float PosYRange = 8f;
    public float RotationRange = 10f;
    public Text MoneyText;
    public Text DustText;
    public GameObject MoneyHUD;
    public GameObject DustHUD;
    public PackOpeningArea OpeningArea;

    public int StartingAmountOfDust = 1000;
    public int StartingAmountOfMoney = 1000;

    public static ShopManager Instance;
    public int PacksCreated { get; set;}
    private float packPlacementOffset = -0.01f;

    public GameObject ShopPanel;
    public GameObject ShopCreaturePrefab;

    public GameObject FilterButton;
    public RarityOptions RarityFilter;

    CardAsset asset;

    private List<GameObject> CreatedCards = new List<GameObject>();

 
    void Awake()
    {
        Instance = this;
        //HideScreen();

        //load unopened packs, if they exist

        //if (PlayerPrefs.HasKey("UnopenedPacks"))
        //{
        //    Debug.Log("UnopenedPacks: " + PlayerPrefs.GetInt("UnopenedPacks"));
        //    StartCoroutine(GivePacks(PlayerPrefs.GetInt("UnopenedPacks"), true));
        //}
            
        //actually, load dust and money FROM PlayerPrefs
        LoadDustAndMoneyToPlayerPrefs();

        
        RarityFilter = RarityOptions.Common;

    }

    void Start()
    {
        //CreateCards(CardCollection.Instance.GetAllCards());
    }

    private void CreateCards(List<CardAsset> listca)
    {
        ClearCreatedCards();
        foreach (CardAsset ca in listca)
        {
            GameObject go = Instantiate(ShopCreaturePrefab,ShopPanel.transform);
            go.GetComponent<Image>().sprite = ca.HeroPortrait;
            go.GetComponentInChildren<Text>().text = ca.name;
            CreatedCards.Add(go);

//            OneCardManager manager = go.GetComponent<OneCardManager>();
//            manager.cardAsset = ca;


            //DS: if player owns the creature, deactivate the GO

            if (CardCollection.Instance.QuantityOfEachCard[ca] > 0)
            {
                //go.GetComponent<Button>().interactable = false;
            }


            //manager.ReadCardFromAsset();

            AddCardToCollection addCardComponent = go.GetComponent<AddCardToCollection>();
            addCardComponent.SetCardAsset(ca);
        }


    }

    public void ClearCreatedCards()
    {
        while(CreatedCards.Count>0)
        {
            GameObject g = CreatedCards[0];
            CreatedCards.RemoveAt(0);
            Destroy(g);
        }
    }

    private int money; 
    public int Money
    {
        get{ return money; }
        set
        {
            money = value;
            MoneyText.text = money.ToString();
        }
    }

    private int dust; 
    public int Dust
    {
        get{ return dust; }
        set
        {
            dust = value;
            DustText.text = dust.ToString();
        }
    }

    public void BuyPack()
    {
        if (money >= PackPrice)
        {
            Money -= PackPrice;
            StartCoroutine(GivePacks(1));
        }
    }

    public IEnumerator GivePacks(int NumberOfPacks, bool instant = false)
    {
        for (int i = 0; i < NumberOfPacks; i++)
        {
            GameObject newPack = Instantiate(PackPrefab, PacksParent);

            //pack placement offset is to ensure that colliders don't overlap
            Vector3 localPositionForNewPack = new Vector3(Random.Range(-PosXRange, PosXRange), Random.Range(-PosYRange, PosYRange), PacksCreated*packPlacementOffset);
            newPack.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(-RotationRange, RotationRange));
            PacksCreated++;

            // make this pack appear on top of all the previous packs using PacksCreated;
            newPack.GetComponentInChildren<Canvas>().sortingOrder = PacksCreated;
            
            //if instant, no animation. else, show tween effect
            if (instant)
                newPack.transform.localPosition = localPositionForNewPack;
            else
            {
                newPack.transform.position = InitialPackSpot.position;
                newPack.transform.DOLocalMove(localPositionForNewPack, 0.5f);
                yield return new WaitForSeconds(0.5f);
            }

        }
        yield break;
    }

    void OnApplicationQuit()
    {
        SaveDustAndMoneyToPlayerPrefs();

        PlayerPrefs.SetInt("UnopenedPacks", PacksCreated);
    }

    public void LoadDustAndMoneyToPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("Dust"))
            Dust = PlayerPrefs.GetInt("Dust");
        else
            Dust = StartingAmountOfDust;  // default value of dust to give to player

        if (PlayerPrefs.HasKey("Money"))
            Money = PlayerPrefs.GetInt("Money");
        else
            Money = StartingAmountOfMoney;  // default value of dust to give to player
    }
        
    public void SaveDustAndMoneyToPlayerPrefs()
    {
        PlayerPrefs.SetInt("Dust", dust);
        PlayerPrefs.SetInt("Money", money);
    }

    public void ShowScreen()
    {
        ScreenContent.SetActive(true);
        //MoneyHUD.SetActive(true);
    }

    public void HideScreen()
    {
        ScreenContent.SetActive(false);
        //MoneyHUD.SetActive(false);
    }

    public void BuyCard(GameObject go)
    {

        if (go.GetComponent<AddCardToCollection>() != null)
            asset = go.GetComponent<AddCardToCollection>().cardAsset;
        else
            asset = go.GetComponent<AddCardToDeck>().cardAsset;

        if (dust >= asset.cardCost)
        {
            Dust -= asset.cardCost;
            Debug.Log ("Buying " + asset.name + " for " + asset.cardCost + " gold!");
        }

        if ( GetComponent<CollectionBrowser>() != null)
        {
            GetComponent<CollectionBrowser>().ActivateCreatureInCollection(asset);

            //go.GetComponent<Button>().interactable = false;

            PlayerPrefs.SetInt("Dust", dust);
        }

        else
        {
            go.GetComponentInParent<CollectionBrowser>().ActivateCreatureInCollection(asset);
            go.GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("Dust", dust);
        }


    }

    public void SellCard(GameObject go)
    {

        if (go.GetComponent<AddCardToCollection>() != null)
            asset = go.GetComponent<AddCardToCollection>().cardAsset;
        else
            asset = go.GetComponent<AddCardToDeck>().cardAsset;


            Dust += asset.cardCost / 2;

        if ( GetComponent<CollectionBrowser>() != null)
        {
            GetComponent<CollectionBrowser>().RemoveCreatureInCollection(asset);

            //go.GetComponent<Button>().interactable = false;

            PlayerPrefs.SetInt("Dust", dust);
        }

        else
        {
            go.GetComponentInParent<CollectionBrowser>().RemoveCreatureInCollection(asset);
            PlayerPrefs.SetInt("Dust", dust);
        }


    }


}
