using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroSelect
{
	Pick,
	Fixed,
	Random
}

public class CampaignAsset : ScriptableObject {

	[Header("Heroes")]

	public HeroSelect AlliesSelect = HeroSelect.Fixed;
	public List<CardAsset> fixedAllies;

	public List<CardAsset> fixedEnemies;

	[Header("Rewards")]
	public int gold;
	public int dust;
	public List<CardAsset> cards;
}
