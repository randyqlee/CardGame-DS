using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBrowser : MonoBehaviour {

    public Transform[] Slots;
    public GameObject SpellMenuPrefab;
    public GameObject CreatureMenuPrefab;

    public GameObject OneCharacterTabs;
    public GameObject AllCharactersTabs;

    //DS
    public GameObject CollectionPanel;
    public GameObject CollectionCreaturePrefab;

    public GameObject LockedCardsPanel;

    public GameObject FilterButton;
    public RarityOptions RarityFilter;

    public Material glowCommon;
    public Material glowRare;
    public Material glowEpic;
    public Material glowLegendary;



    public KeywordInputField KeywordInputFieldScript;
    public CardsThatYouDoNotHaveToggle CardsThatYouDoNotHaveToggleScript;
    public ManaFilter ManaFilterSctipt;

    private CharacterAsset _character;

    public List<GameObject> CreatedCards = new List<GameObject>();
    public List<GameObject> OwnedCards = new List<GameObject>();
    public List<GameObject> LockedCards = new List<GameObject>();

    //private CardsDisplayInfo InfoAboutLastShownPage;

    // PROPERTIES for every variable that matters for filtering and selecting cards:
    #region PROPERTIES
    private bool _showingCardsPlayerDoesNotOwn = false;
    public bool ShowingCardsPlayerDoesNotOwn
    {
        get { return _showingCardsPlayerDoesNotOwn;}

        set
        {
            _showingCardsPlayerDoesNotOwn = value;
            UpdatePage();
        }
    }

    private int _pageIndex = 0;
    public int PageIndex 
    {
        get{ return _pageIndex;}
        set
        {
            _pageIndex = value;
            UpdatePage();
        }
    }

    private bool _includeAllRarities = true;
    public bool IncludeAllRarities
    {
        get{ return _includeAllRarities; }
        set
        {
            _includeAllRarities = value;
            UpdatePage();
        }
    }

    private bool _includeAllCharacters = true; 
    public bool IncludeAllCharacters
    {
        get{ return _includeAllCharacters; }
        set
        {
            _includeAllCharacters = value;
            // show the first page in order for cards to be instantly visible
            _pageIndex = 0;
            UpdatePage();
        }
    }

    private RarityOptions _rarity = RarityOptions.Basic;  // since includeAllRarities is always true, this rarity field will not matter
    public RarityOptions Rarity 
    {
        get{ return _rarity; }
        set
        {
            _rarity = value;
            UpdatePage();
        }
    }

    private CharacterAsset _asset = null; 
    public CharacterAsset Asset
    {
        get{ return _asset; }
        set
        {
            _asset = value;
            // show the first page in order for cards to be instantly visible
            _pageIndex = 0;
            UpdatePage();
        }
    }

    private string _keyword = "";
    public string Keyword
    {
        get{ return _keyword;}
        set
        {
            _keyword = value;
            UpdatePage();
        }
    }

    private int _manaCost = -1; 
    public int ManaCost
    {
        get{ return _manaCost; }
        set
        {
            _manaCost = value;
            _pageIndex = 0;
            UpdatePage();
        }
    }

    private bool _includeTokenCards = false; 
    public bool IncludeTokenCards
    {
        get{ return _includeTokenCards; }
        set
        {
            _includeTokenCards = value;
            UpdatePage();
        }
    }
    #endregion

    public void ShowCollectionForBrowsing()
    {
        // reset keyword input field, reset toggle, reset mana filter:
        KeywordInputFieldScript.Clear();
        CardsThatYouDoNotHaveToggleScript.SetValue(false);
        ManaFilterSctipt.RemoveAllFilters();

        ShowCards(false, 0, true, false, RarityOptions.Basic, null, "", -1, false);

        // select neutral tab by default:
        DeckBuildingScreen.Instance.TabsScript.NeutralTabWhenCollectionBrowsing.Select(instant: true);   
        DeckBuildingScreen.Instance.TabsScript.SelectTab(DeckBuildingScreen.Instance.TabsScript.NeutralTabWhenCollectionBrowsing, instant: true);
    }

    public void ShowCollectionForDeckBuilding(CharacterAsset buildingForThisCharacter)
    {
        // reset keyword input field, reset toggle, reset mana filter:
        KeywordInputFieldScript.Clear();
        CardsThatYouDoNotHaveToggleScript.SetValue(false);
        ManaFilterSctipt.RemoveAllFilters();

        _character = buildingForThisCharacter;

        ShowCards(false, 0, true, false, RarityOptions.Basic, _character, "", -1, false);

        // select a tab with class cards by default
        DeckBuildingScreen.Instance.TabsScript.ClassTab.Select(instant: true);
        DeckBuildingScreen.Instance.TabsScript.SelectTab(DeckBuildingScreen.Instance.TabsScript.ClassTab, instant: true);
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

    public void UpdateQuantitiesOnPage()
    {
        foreach (GameObject card in CreatedCards)
        {
            AddCardToDeck addCardComponent = card.GetComponent<AddCardToDeck>();
            addCardComponent.UpdateQuantity();
        }
    }

    // a method to display cards based on all the selected parameters
    public void UpdatePage()
    {
        ShowCards(_showingCardsPlayerDoesNotOwn, _pageIndex, _includeAllRarities, _includeAllCharacters, _rarity, _asset, _keyword, _manaCost, _includeTokenCards);
    }

    private void ShowCards(bool showingCardsPlayerDoesNotOwn = false, int pageIndex = 0, bool includeAllRarities = true, bool includeAllCharacters = true, 
        RarityOptions rarity = RarityOptions.Basic, CharacterAsset asset = null, string keyword = "", int manaCost = -1, bool includeTokenCards = false)
    {
        // saving the information about the cards that we are showing to players on this page
        _showingCardsPlayerDoesNotOwn = showingCardsPlayerDoesNotOwn;
        _pageIndex = pageIndex;
        _includeAllRarities = includeAllRarities;
        _includeAllCharacters = includeAllCharacters;
        _rarity = rarity;
        _asset = asset;
        _keyword = keyword;
        _manaCost = manaCost;
        _includeTokenCards = includeTokenCards;
        
        List<CardAsset> CardsOnThisPage = PageSelection(showingCardsPlayerDoesNotOwn, pageIndex, includeAllRarities, includeAllCharacters, rarity,
            asset, keyword, manaCost, includeTokenCards);

        // clear created cards list 
        ClearCreatedCards();

        if (CardsOnThisPage.Count == 0)
            return;
        
        // Debug.Log(CardsOnThisPage.Count);

        for (int i = 0; i < CardsOnThisPage.Count; i++)
        {
            GameObject newMenuCard;

            if (CardsOnThisPage[i].TypeOfCard == TypesOfCards.Creature)
            {
                // it is a creature card
                newMenuCard = Instantiate(CreatureMenuPrefab, Slots[i].position, Quaternion.identity) as GameObject;
            }
            else
            {
                // it is a spell card
                newMenuCard = Instantiate(SpellMenuPrefab, Slots[i].position, Quaternion.identity) as GameObject;
            }

            newMenuCard.transform.SetParent(this.transform);

            CreatedCards.Add(newMenuCard);

            //OneCardManager manager = newMenuCard.GetComponent<OneCardManager>();
            //manager.cardAsset = CardsOnThisPage[i];
            //manager.ReadCardFromAsset();

            AddCardToDeck addCardComponent = newMenuCard.GetComponent<AddCardToDeck>();
            addCardComponent.SetCardAsset(CardsOnThisPage[i]);
            addCardComponent.UpdateQuantity();
        }
    }

    public void Next()
    {
        if (PageSelection(_showingCardsPlayerDoesNotOwn, _pageIndex+1,_includeAllRarities, _includeAllCharacters, _rarity,
            _asset,_keyword,_manaCost, _includeTokenCards).Count == 0)
            return;
        
        ShowCards(_showingCardsPlayerDoesNotOwn, _pageIndex+1,_includeAllRarities, _includeAllCharacters, _rarity,
            _asset,_keyword,_manaCost, _includeTokenCards);
    }

    public void Previous()
    {
        if (_pageIndex == 0)
            return;

        ShowCards(_showingCardsPlayerDoesNotOwn, _pageIndex-1, _includeAllRarities, _includeAllCharacters, _rarity,
            _asset, _keyword, _manaCost, _includeTokenCards);
    }

    // Returns a list with assets of cards that we have to show on page with pageIndex that. Selects cards that satisfy all the other parameters (rarity, manaCost, etc...)
    private List<CardAsset> PageSelection(bool showingCardsPlayerDoesNotOwn = false, int pageIndex = 0, bool includeAllRarities = true, bool includeAllCharacters = true, 
        RarityOptions rarity = RarityOptions.Basic, CharacterAsset asset = null, string keyword = "", int manaCost = -1, bool includeTokenCards = false)
    {
        List<CardAsset> returnList = new List<CardAsset>();

        // obtain cards from collection that satisfy all the selected criteria
        List<CardAsset> cardsToChooseFrom = CardCollection.Instance.GetCards(showingCardsPlayerDoesNotOwn, includeAllRarities, includeAllCharacters, rarity,
            asset, keyword, manaCost, includeTokenCards);

        // if there are enough cards so that we can show some cards on page with pageIndex
        // otherwise an empty list will be returned
        if (cardsToChooseFrom.Count > pageIndex * Slots.Length)
        {
            // the following for loop has 2 conditions for counter i:
            // 1) i < cardsToChooseFrom.Count - pageIndex * Slots.Length checks that we did not run out on cards on the last page 
            // (for example, there are 10 slots on the page, but we only have to show 5 cards) 
            // 2) i < Slots.Length checks that we have reached the limit of cards to display on one page (filled the whole page)
            for (int i = 0; (i < cardsToChooseFrom.Count - pageIndex * Slots.Length && i < Slots.Length); i++)
            {
                returnList.Add(cardsToChooseFrom[pageIndex * Slots.Length + i]);
            }
        }

        return returnList;
    }




    //DS
    public void Start()
    {
        CreateCards(CardCollection.Instance.GetAllCards());
        RarityFilter = RarityOptions.Basic; //Basic = ALL Cards
    }


    public void ActivateCreatureInCollection(CardAsset ca)
    {
        foreach(GameObject go in CreatedCards)
        {
            CardAsset asset = go.GetComponent<AddCardToDeck>().cardAsset;
            if (asset == ca)
            {
                //go.GetComponent<Button>().interactable = true;
                //AddCardToDeck addCardComponent = go.GetComponent<AddCardToDeck>();
                //addCardComponent.SetCardAsset(ca);
                //addCardComponent.lockButton.gameObject.SetActive(false);
                //addCardComponent.addButton.gameObject.SetActive(true);
                //addCardComponent.isOwned = true;

                //go.GetComponent<Image>().material = null;

                PlayerPrefs.SetInt("NumberOf" + ca.name, CardCollection.Instance.QuantityOfEachCard[ca]++);

                OwnedCards.Add(go);
                LockedCards.Remove(go);
            }
     
        }

        CreateCards(CardCollection.Instance.GetAllCards());
    }

    public void RemoveCreatureInCollection(CardAsset ca)
    {
        foreach(GameObject go in CreatedCards)
        {
            CardAsset asset = go.GetComponent<AddCardToDeck>().cardAsset;
            if (asset == ca)
            {
                //go.GetComponent<Button>().interactable = true;
                //AddCardToDeck addCardComponent = go.GetComponent<AddCardToDeck>();
                //addCardComponent.SetCardAsset(ca);
                //addCardComponent.lockButton.gameObject.SetActive(true);
                //addCardComponent.addButton.gameObject.SetActive(false);
                //addCardComponent.isOwned = false;

                //go.GetComponent<Image>().material = go.GetComponent<CollectionCreaturePrefab>().material;

                PlayerPrefs.SetInt("NumberOf" + ca.name, CardCollection.Instance.QuantityOfEachCard[ca]--);

                LockedCards.Add(go);
                OwnedCards.Remove(go);    

                //DeckBuilder.Instance.RemoveCardFromAllDecks(ca);            
            }            
        }

        CreateCards(CardCollection.Instance.GetAllCards());

        
    }

    private void CreateCards(List<CardAsset> listca)
    {
        ClearCreatedCards();
        OwnedCards = new List<GameObject>();
        LockedCards = new List<GameObject>();
        
        foreach (CardAsset ca in listca)
        {
            GameObject go;
            //GameObject go = Instantiate(CollectionCreaturePrefab,CollectionPanel.transform);

            if (CardCollection.Instance.QuantityOfEachCard[ca] == 0)
            {
                go = Instantiate(CollectionCreaturePrefab,LockedCardsPanel.transform);

                
                AddCardToDeck addCardComponent = go.GetComponent<AddCardToDeck>();
                addCardComponent.SetCardAsset(ca);

                addCardComponent.addButton.gameObject.SetActive(false);
                addCardComponent.removeButton.gameObject.SetActive(false);
                addCardComponent.lockButton.gameObject.SetActive(true);

                addCardComponent.isOwned = false;
                LockedCards.Add(go);

                go.GetComponent<Image>().material = go.GetComponent<CollectionCreaturePrefab>().material;



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

                CreatedCards.Add(go);

            }
            


            else
            {
                go = Instantiate(CollectionCreaturePrefab,CollectionPanel.transform);
                AddCardToDeck addCardComponent = go.GetComponent<AddCardToDeck>();
                addCardComponent.SetCardAsset(ca);

                addCardComponent.addButton.gameObject.SetActive(true);
                addCardComponent.removeButton.gameObject.SetActive(false);
                addCardComponent.lockButton.gameObject.SetActive(false);

                addCardComponent.isOwned = true;
                OwnedCards.Add(go);

                go.GetComponent<Image>().material = null;

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

                CreatedCards.Add(go);
            }
        }


    }
    
    public void PressOnFilter()
    {
        switch (RarityFilter)
        {
            case RarityOptions.Common:
                RarityFilter = RarityOptions.Rare;
                FilterButton.GetComponentInChildren<Text>().text = "Rare";
                CreateCards(CardCollection.Instance.GetRarityCards(RarityFilter));                
                break;
            
            case RarityOptions.Rare:
                RarityFilter = RarityOptions.Epic;
                FilterButton.GetComponentInChildren<Text>().text = "Epic";
                CreateCards(CardCollection.Instance.GetRarityCards(RarityFilter));
                break;
            
            case RarityOptions.Epic:
                RarityFilter = RarityOptions.Legendary;
                FilterButton.GetComponentInChildren<Text>().text = "Legendary";
                CreateCards(CardCollection.Instance.GetRarityCards(RarityFilter));
                break;
            
            case RarityOptions.Legendary:
                RarityFilter = RarityOptions.Basic;
                FilterButton.GetComponentInChildren<Text>().text = "ALL";
                CreateCards(CardCollection.Instance.GetAllCards());
                break;
            case RarityOptions.Basic:                
                RarityFilter = RarityOptions.Common;
                FilterButton.GetComponentInChildren<Text>().text = "Common";
                CreateCards(CardCollection.Instance.GetRarityCards(RarityFilter));
                break;

            

        }

    }
}

