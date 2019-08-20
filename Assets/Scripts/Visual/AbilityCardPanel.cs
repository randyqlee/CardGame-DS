using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCardPanel : MonoBehaviour {

	//public int abilityNo;
	public Text cardTitleText;
	public Text descriptionText;
	public Image cardGraphicImage;	
	public Text manaText;
	public Image cardGraphicBackground;
	
	
	// Use this for initialization
	void Awake () {
		//Setup();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Setup()
	{
		int creatureID = GetComponentInParent<IDHolder>().UniqueID;
		GameObject creature = IDHolder.GetGameObjectWithID(creatureID);
		CreatureLogic cl = CreatureLogic.CreaturesCreatedThisGame[creatureID];
		foreach (CreatureEffect ce in cl.creatureEffects)
		{
			
				cardTitleText.text = ce.abilityDescription;
				cardGraphicImage.sprite = ce.abilityPreviewSprite;
				manaText.text = ce.creatureEffectCooldown.ToString();
				cardTitleText.text = ce.Name;
				descriptionText.text = ce.abilityDescription;
			

			
		}

	}
}
