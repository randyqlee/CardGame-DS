using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Target
{
	Enemies,
	Allies,
	Any,
	

}

public enum Type
{
	Active,
	Passive,
	Silenced,
	Stunned,
	ExtraTurn
}
public class AbilityAsset : ScriptableObject {

	public string abilityName;
	public Sprite icon;

	[TextArea(5,10)]
	public string description;

	[Header("Ability Script name - must be typed correctly")]
	public string abilityEffect;

	public int abilityCoolDown;

	public Type skillType = Type.Passive;
	public Target target = Target.Any;

	[Header("Character Equip Script name - must be typed correctly")]
	public string characterEffect;
	public int AtkPctValue;
	public int HealthPctValue;
	public int ChancePctValue;


//	[Header("Ability Buffs")]
//	public List<AbilityBuffs> abilityBuffs;

	//since Bufflist buff is an enum, calling method should use enum.GetName() to convert the enum to its string text, so we can use gameObject.AddComponent(System.Type.GetType(buffName));



//	[Header("Ability Debuffs")]
//	public List<AbilityDebuffs> abilityDebuffs;

}
