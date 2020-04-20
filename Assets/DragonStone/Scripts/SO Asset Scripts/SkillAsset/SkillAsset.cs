using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Chance,
    Ultimate,
	Rune
}

public class SkillAsset : ScriptableObject
{
	public string abilityName;
	public Sprite icon;

    [TextArea(2,3)]
	public string description;

	public string abilityEffect;

	public int abilityCoolDown;

	public SkillType skillType;

}
