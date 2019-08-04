//DS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AbilityEffect {

	public string CreatureScriptName;
	public int coolDown;

	public Sprite abilityImage;
	public GameObject specialEffects;	
	
	[Header("Card Information")]
	public int abilityCost;
	[TextArea(5,20)]
	public string abilityDescription;



	
}
