using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCardPanel : MonoBehaviour {

	public List<GameObject> abilityCards = new List<GameObject>();
	
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

	void Setup()
	{
		int x = 0;
		int creatureID = GetComponentInParent<IDHolder>().UniqueID;
		GameObject creature = IDHolder.GetGameObjectWithID(creatureID);
		CreatureLogic cl = CreatureLogic.CreaturesCreatedThisGame[creatureID];
		foreach (CreatureEffect ce in cl.creatureEffects)
		{
			
				abilityCards[x].GetComponent<AbilityCardPanel>().cardTitleText.text = ce.abilityDescription;
				abilityCards[x].GetComponent<AbilityCardPanel>().cardGraphicImage.sprite = ce.abilityPreviewSprite;
				abilityCards[x].GetComponent<AbilityCardPanel>().manaText.text = ce.creatureEffectCooldown.ToString();
				abilityCards[x].GetComponent<AbilityCardPanel>().cardTitleText.text = ce.Name;
				abilityCards[x].GetComponent<AbilityCardPanel>().descriptionText.text = ce.abilityDescription;	
				x++;
			
		}

	}
}
