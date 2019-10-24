using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuilder : MonoBehaviour 
{
    public GameObject CardNamePrefab;
    public Transform Content;

    //DS

    public static DeckBuilder Instance;
    public GameObject DeckPanel;
    public GameObject DeckCreaturePrefab;

    public List<GameObject> goDeckList = new List<GameObject>();

    public List<GameObject> deckButtons = new List<GameObject>();

    public GameObject playBtn;


    public InputField DeckName;

    public int SameCardLimit = 2;
    public int AmountOfCardsInDeck = 10;

    public GameObject DeckCompleteFrame;

    private List<CardAsset> deckList = new List<CardAsset>();

    private Dictionary<CardAsset, CardNameRibbon> ribbons = new Dictionary<CardAsset, CardNameRibbon>();

    public bool InDeckBuildingMode{ get; set;}
    private CharacterAsset buildingForCharacter;

    public static int deckNumber;

    public int currentDeckNumber {
        get
        { return deckNumber; }
    }


    void Awake()
    {
        Instance = this;
        deckNumber = 1;
        playBtn.GetComponent<Button>().interactable = false;
        //DeckCompleteFrame.GetComponent<Image>().raycastTarget = false;
    }

    void Start()
    {
        //LoadDecks();
        
        ShowDeck1();
    }


// DS
/*
    public void AddCard(CardAsset asset)
    {
        // if we are browsing the collection
        if (!InDeckBuildingMode)
            return;

        // if the deck is already full 
        if (deckList.Count == AmountOfCardsInDeck)
            return;

        int count = NumberOfThisCardInDeck(asset);

        int limitOfThisCardInDeck = SameCardLimit;

        // if something else is specified in the CardAsset, we use that.
//        if (asset.OverrideLimitOfThisCardInDeck > 0)
//            limitOfThisCardInDeck = asset.OverrideLimitOfThisCardInDeck;

        if (count < limitOfThisCardInDeck)
        {
            deckList.Add(asset);

            CheckDeckCompleteFrame();

            // added one to count if we are adding this card
            count++;

            // do all the graphical stuff
            if (ribbons.ContainsKey(asset))
            {
                // update quantity 
                ribbons[asset].SetQuantity(count);
            }
            else
            {
                // 1) Add card`s name to the list
                GameObject cardName = Instantiate(CardNamePrefab, Content) as GameObject;
                cardName.transform.SetAsLastSibling();
                cardName.transform.localScale = Vector3.one;
                CardNameRibbon ribbon = cardName.GetComponent<CardNameRibbon>();
                ribbon.ApplyAsset(asset, count);
                ribbons.Add(asset, ribbon);
            }
        }
    }
*/

    void CheckDeckCompleteFrame()
    {
        DeckCompleteFrame.SetActive(deckList.Count == AmountOfCardsInDeck);
    }

    public void AddCard(CardAsset ca)
    {

        if (deckList.Count == AmountOfCardsInDeck)
            return;


        GameObject go = Instantiate(DeckCreaturePrefab,DeckPanel.transform);
        //go.GetComponent<Image>().sprite = ca.HeroPortrait;
        //go.GetComponentInChildren<Text>().text = ca.name;
        go.GetComponent<CollectionCreaturePrefab>().creatureImage.GetComponent<Image>().sprite = ca.HeroPortrait;
        go.GetComponent<CollectionCreaturePrefab>().creatureText.GetComponent<Text>().text = ca.name;

        Image frame = go.GetComponent<CollectionCreaturePrefab>().glowImage.GetComponent<Image>();
                

        if (ca.Rarity == RarityOptions.Common)
        {
            //frame.material = glowCommon;
            frame.color = Color.white;

        }
        if (ca.Rarity == RarityOptions.Rare)
        {
            //frame.material = glowRare;
            frame.color = Color.green;

        }
                    if (ca.Rarity == RarityOptions.Epic)
        {
            //frame.material = glowEpic;
            frame.color = Color.magenta;

        }
                    if (ca.Rarity == RarityOptions.Legendary)
        {
            //frame.material = glowLegendary;
            frame.color = Color.yellow;

        }

        deckList.Add(ca);

        goDeckList.Add(go);


        //OneCardManager manager = go.GetComponent<OneCardManager>();
        //manager.cardAsset = ca;
        //manager.ReadCardFromAsset();

        AddCardToDeck addCardComponent = go.GetComponent<AddCardToDeck>();
        addCardComponent.SetCardAsset(ca);
        addCardComponent.isAdded = true;

        //UpdateDeck();
        UpdateDeck(deckNumber);



        //UpdateCollectionPanel();


    }

    public int NumberOfThisCardInDeck (CardAsset asset)
    {
        int count = 0;
        foreach (CardAsset ca in deckList)
        {
            if (ca == asset)
                count++;
        }
        return count;
    }

    public void RemoveCard(CardAsset asset)
    {
        Debug.Log("InRemoveCard");
        CardNameRibbon ribbonToRemove = ribbons[asset];
        ribbonToRemove.SetQuantity(ribbonToRemove.Quantity-1);

        if (NumberOfThisCardInDeck(asset) == 1)
        {            
            ribbons.Remove(asset);
            Destroy(ribbonToRemove.gameObject);
        }

        deckList.Remove(asset);

        CheckDeckCompleteFrame();

        // update quantities of all cards that we currently show in the collection
        // this should be after deckList.Remove(asset); line to show correct quantities
        DeckBuildingScreen.Instance.CollectionBrowserScript.UpdateQuantitiesOnPage();
    }

    public void RemoveCard(GameObject go)
    {
        CardAsset asset = go.GetComponent<AddCardToDeck>().cardAsset;

        deckList.Remove(asset);
        goDeckList.Remove(go);
        Destroy(go);

        //UpdateDeck();
        UpdateDeck(deckNumber);

    }

    public void RemoveCardFromAllDecks(CardAsset asset)
    {
        foreach (DeckInfo di in  DecksStorage.Instance.AllDecks)
        {
            for (int i = di.Cards.Count - 1 ; i >= 0; i--)
            {
                if ( asset == di.Cards[i] )
                {
                    di.Cards.Remove(di.Cards[i]);
                }
            }
        }

        for (int i=0; i<DecksStorage.Instance.AllDecks.Count; i++)
        {
            UpdateDeck(i+1);
        }





        if (deckNumber == 1)
        ShowDeck1();
        else if(deckNumber == 2)
        ShowDeck2();
        else if(deckNumber == 3)
        ShowDeck3();

    }

    public void RemoveCardFromAllDecks(GameObject go)
    {

        CardAsset asset = go.GetComponent<AddCardToDeck>().cardAsset;

        foreach (DeckInfo di in  DecksStorage.Instance.AllDecks)
        {
            for (int i = di.Cards.Count - 1 ; i >= 0; i--)
            {
                if (asset == di.Cards[i])
                    RemoveCard(go);
            }
        }
    }

    public void BuildADeckFor(CharacterAsset asset)
    {
        InDeckBuildingMode = true;
        buildingForCharacter = asset;
        // TODO: remove all the cards from the deck list if there are any, 
        // both the actual list and visually delete all the card list items
        while (deckList.Count>0)
        {
            RemoveCard(deckList[0]);
        }

        // apply character class and activate tab.
        DeckBuildingScreen.Instance.TabsScript.SetClassOnClassTab(asset);
        DeckBuildingScreen.Instance.CollectionBrowserScript.ShowCollectionForDeckBuilding(asset);

        CheckDeckCompleteFrame();

        // reset the InputField text to be empty
        DeckName.text = "";
    }

    public void DoneButtonHandler()
    {
        // save current deck list, character and deck name into DecksStorage
        DeckInfo deckToSave = new DeckInfo(deckList, DeckName.text, buildingForCharacter);
        DecksStorage.Instance.AllDecks.Add(deckToSave);
        DecksStorage.Instance.SaveDecksIntoPlayerPrefs();
        // the screen with collection and pre-made decks is loaded by calling other methods on this button
        DeckBuildingScreen.Instance.ShowScreenForCollectionBrowsing();
    }

    void OnApplicationQuit()
    {
        // if we exit the app while editing a deck, we want to save it anyway
       //DS
       // DoneButtonHandler();
    }


    //DS
    public void Play()
    {
        if(deckList.Count >= AmountOfCardsInDeck)
            BattleStartInfo.SelectedDeck = new DeckInfo(deckList);

        //DS TODO: Make PLAY Button non-interactable when Deck has less than sufficient cards
    }

    public void UpdateDeck()
    {
        if (DecksStorage.Instance.AllDecks.Count == 0 )
        {
            DecksStorage.Instance.AllDecks.Add(new DeckInfo(deckList));

        }
        else
            DecksStorage.Instance.AllDecks[0] = new DeckInfo(deckList);

        DecksStorage.Instance.SaveDecksIntoPlayerPrefs();

    }

    public void UpdateDeck(int deckNum)
    {
        playBtn.GetComponent<Button>().interactable = false;

        if (DecksStorage.Instance.AllDecks.Count == 0 )
        {
            DecksStorage.Instance.AllDecks.Add(new DeckInfo(deckList));

        }
        else
            DecksStorage.Instance.AllDecks[0] = new DeckInfo(deckList);

        DecksStorage.Instance.SaveDecksIntoPlayerPrefs(deckNum);

        if(DecksStorage.Instance.AllDecks[0].Cards.Count == AmountOfCardsInDeck)
        {
            playBtn.GetComponent<Button>().interactable = true;
        }

        foreach(GameObject ownedGO in GetComponent<CollectionBrowser>().OwnedCards)
            {
                ownedGO.SetActive(true);
            }


       UpdateCollectionPanel();
    }

    public void LoadDecks()
    {
       DecksStorage.Instance.LoadDecksFromPlayerPrefs(); 

       foreach (DeckInfo di in  DecksStorage.Instance.AllDecks)
       {
           foreach (CardAsset ca in di.Cards)
           {
               AddCard(ca);
           }
       }
    }

    public void ClearDeck()
    {

        while(goDeckList.Count>0)
        {
            GameObject go = goDeckList[0];
            
        CardAsset asset = go.GetComponent<AddCardToDeck>().cardAsset;

        deckList.Remove(asset);
        goDeckList.Remove(go);
        Destroy(go);
        }
    }

    public void LoadDeck(int deckNo)
    {
       DecksStorage.Instance.LoadDecksFromPlayerPrefs(deckNo); 

       foreach (DeckInfo di in  DecksStorage.Instance.AllDecks)
       {
           foreach (CardAsset ca in di.Cards)
           {
               AddCard(ca);
           }
       }

       foreach(GameObject ownedGO in GetComponent<CollectionBrowser>().OwnedCards)
            {
                ownedGO.SetActive(true);
            }


       UpdateCollectionPanel();
    }

    public void ShowDeck1()
    {

        playBtn.GetComponent<Button>().interactable = false;

        foreach (GameObject go in deckButtons)
        {
            go.GetComponent<Image>().color = Color.white;
        }

        deckButtons[0].GetComponent<Image>().color = Color.yellow;

        ClearDeck();

        deckNumber = 1;

        LoadDeck(deckNumber);

    }

    public void ShowDeck2()
    {

        playBtn.GetComponent<Button>().interactable = false;

        foreach (GameObject go in deckButtons)
        {
            go.GetComponent<Image>().color = Color.white;
        }

        deckButtons[1].GetComponent<Image>().color = Color.yellow;

        ClearDeck();

        deckNumber = 2;

        LoadDeck(deckNumber);

    }

    public void ShowDeck3()
    {

        playBtn.GetComponent<Button>().interactable = false;
        
        foreach (GameObject go in deckButtons)
        {
            go.GetComponent<Image>().color = Color.white;
        }

        deckButtons[2].GetComponent<Image>().color = Color.yellow;

        ClearDeck();

        deckNumber = 3;

        LoadDeck(deckNumber);

    }

    public void UpdateCollectionPanel()
    {
        foreach(GameObject go in goDeckList)
        {
            foreach(GameObject ownedGO in GetComponent<CollectionBrowser>().OwnedCards)
            {
                
                if (go.GetComponent<AddCardToDeck>().cardAsset == ownedGO.GetComponent<AddCardToDeck>().cardAsset)
                {
                    ownedGO.SetActive(false);
                }

            }
        }
    }
}
