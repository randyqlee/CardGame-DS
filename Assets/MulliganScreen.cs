using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MulliganScreen : MonoBehaviour {


	public List<GameObject> slots;
	public List<GameObject> slots_TOP;

	public List<GameObject> coreGameObjects;
	// Use this for initialization

	public Deck deck;
	public Deck deck_TOP;

	public List<CardAsset> battleCreatures;
	public List<CardAsset> equipCreatures;

	public Button fightButton;

	void Awake()
	{

		fightButton.interactable = false;
	}

	void Start () {

		for (int i = 0; i < deck.cards.Count; i++)
		{
			GameObject go = Instantiate(GlobalSettings.Instance.MulliganCreaturePrefab, slots[i].transform) as GameObject;
			go.GetComponent<OneCreatureManager>().cardAsset = deck.cards[i];
			go.GetComponent<OneCreatureManager>().ReadCreatureFromAsset();
			CardAsset ca = go.GetComponent<OneCreatureManager>().cardAsset;
			battleCreatures.Add(ca);
		}

		//Show the deck of enemy (TOP player)
		for (int i = 0; i < deck_TOP.cards.Count; i++)
		{
			GameObject go = Instantiate(GlobalSettings.Instance.TOP_MulliganCreaturePrefab, slots_TOP[i].transform) as GameObject;
			go.GetComponent<OneCreatureManager>().cardAsset = deck_TOP.cards[i];
			go.GetComponent<OneCreatureManager>().ReadCreatureFromAsset();
			//CardAsset ca = go.GetComponent<OneCreatureManager>().cardAsset;
			//battleCreatures.Add(ca);
		}
	}

	public void SelectEquipCreatures(GameObject go, bool isSelected)
	{
		if (!isSelected)
		{
			equipCreatures.Add(go.GetComponent<OneCreatureManager>().cardAsset);
			battleCreatures.Remove(go.GetComponent<OneCreatureManager>().cardAsset);

			if(battleCreatures.Count == GlobalSettings.Instance.HeroesCount)
			{
				fightButton.interactable = true;

			}

			else
				fightButton.interactable = false;

		}
		else
		{
			equipCreatures.Remove(go.GetComponent<OneCreatureManager>().cardAsset);
			battleCreatures.Add(go.GetComponent<OneCreatureManager>().cardAsset);
			if(battleCreatures.Count == GlobalSettings.Instance.HeroesCount)
			{
				fightButton.interactable = true;
			}

			else
				fightButton.interactable = false;

		}
	}

	public void AcceptMulligan()
	{
		//put code to get desired cards position

		int i = 0;

		foreach (CardAsset ca in battleCreatures)
		{
			deck.cards[i] = ca;
			i++;
		}

		foreach (CardAsset ca in equipCreatures)
		{
			deck.cards[i] = ca;
			i++;
		}


		foreach (GameObject go in coreGameObjects)
		{
			go.SetActive(true);
		}
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
