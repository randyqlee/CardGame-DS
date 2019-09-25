using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour {

	public enum ItemType
	{
		Gold50,
		Gold100
	}

	public ItemType itemType;

	public Text priceText;

	private string defaultText;

	// Use this for initialization
	void Start () {

		defaultText = priceText.text;
		StartCoroutine(LoadPriceRoutine());
		
	}
	
	public void ClickBuy()
	{
		switch (itemType)
		{
			case ItemType.Gold50:
				Purchaser.Instance.Buy50Gold();			
				break;

			case ItemType.Gold100:
				Purchaser.Instance.Buy100Gold();			
				break;
		}
	}

	private IEnumerator LoadPriceRoutine()
	{
		while (!Purchaser.Instance.IsInitialized())
			yield return null;
		
		string loadedPrice = "";

		switch (itemType)
		{
			case ItemType.Gold50:
				loadedPrice = Purchaser.Instance.GetProductPriceFromStore(Purchaser.Instance.GOLD_50);

				break;
			case ItemType.Gold100:
				loadedPrice = Purchaser.Instance.GetProductPriceFromStore(Purchaser.Instance.GOLD_100);
				break;

		}

		priceText.text = defaultText + " " + loadedPrice;
	}
}
