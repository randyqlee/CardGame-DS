using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCardPanelInfoScreen : MonoBehaviour {

	public List<GameObject> abilityCards = new List<GameObject>();
	public Image heroImage;
	public Text heroname;
	public CardAsset cardA;
	
	// Use this for initialization
	void Awake () {
			
			foreach(GameObject go in abilityCards)
			{
				Setup();
			}
			
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Setup()
	{		
		
		//GetCardAsset from Parent
		//cardA = GetComponentInParent<CardInfoScreen>().cardAsset;
		
		
		//DS: Original Script
		// int creatureID = GetComponentInParent<IDHolder>().UniqueID;		
		// GameObject creature = IDHolder.GetGameObjectWithID(creatureID);
		// CreatureLogic cl = CreatureLogic.CreaturesCreatedThisGame[creatureID];

		//Setup Hero Preview			
		heroImage.sprite = cardA.CardImage;		
		heroname.text = cardA.name;

		//Setup Ability Cards
		int x = 0;
		foreach (AbilityAsset aa in cardA.Abilities)
		{
			
				// abilityCards[x].GetComponent<AbilityCardPanelInfoScreen>().cardTitleText.text = ce.abilityDescription;
				// abilityCards[x].GetComponent<AbilityCardPanelInfoScreen>().cardGraphicImage.sprite = ce.abilityPreviewSprite;
				// abilityCards[x].GetComponent<AbilityCardPanelInfoScreen>().manaText.text = ce.creatureEffectCooldown.ToString();
				// abilityCards[x].GetComponent<AbilityCardPanelInfoScreen>().cardTitleText.text = ce.Name;
				// abilityCards[x].GetComponent<AbilityCardPanelInfoScreen>().descriptionText.text = ce.abilityDescription;	
				// x++;

				abilityCards[x].GetComponent<AbilityCardPanelInfoScreen>().cardTitleText.text = aa.abilityName;
				abilityCards[x].GetComponent<AbilityCardPanelInfoScreen>().cardGraphicImage.sprite = aa.icon;
				abilityCards[x].GetComponent<AbilityCardPanelInfoScreen>().manaText.text = aa.abilityCoolDown.ToString();
				abilityCards[x].GetComponent<AbilityCardPanelInfoScreen>().cardTitleText.text = aa.abilityName;
				abilityCards[x].GetComponent<AbilityCardPanelInfoScreen>().descriptionText.text = aa.description;	
				x++;
			
		}

	}
}
