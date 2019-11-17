using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Chance,
    Ultimate
}

[System.Serializable]
public class AbilityAsset {

	public string abilityName;
	public Sprite icon;

	public string description;

	public string abilityEffect;

	public int abilityCoolDown;

	public SkillType skillType;


}
