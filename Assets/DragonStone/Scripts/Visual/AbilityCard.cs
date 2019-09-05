using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCard : MonoBehaviour {

	public Image abilityImage;

	public Text abilityNameText;
	public Text abilityDescriptionText;

	public Text abilityCooldownText;
	public Image CooldownOverlay;

	public GameObject abilityCardPreview;





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateCooldown(int cooldown, int originalCooldown)
	{
		if(cooldown == 0)
		{
			if(abilityCooldownText!=null)
				abilityCooldownText.text = " ";
		}

		else
		{
			if(abilityCooldownText!=null)
				abilityCooldownText.text = cooldown.ToString();
		}

		if(originalCooldown<=0)
		{
			if(CooldownOverlay!=null)
				CooldownOverlay.fillAmount = 0f;
		}		
		else
		{
			float fillValue = ((float)cooldown/(float)originalCooldown);
			if(CooldownOverlay!=null)
				CooldownOverlay.fillAmount = fillValue;
			
		}
		
		

	}
}
