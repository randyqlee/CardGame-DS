using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCard : MonoBehaviour {

	public Image abilityImage;

	public Text abilityNameText;
	public Text abilityDescriptionText;

	public Text abilityCooldownText;





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateCooldown(int cooldown)
	{
		abilityCooldownText.text = cooldown.ToString();

	}
}
