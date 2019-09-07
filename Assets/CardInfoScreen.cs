using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoScreen : MonoBehaviour {


	public Button closeButton;
	public Button buyButton;
	public Text buyText;
	public Button sellButton;
	public Text sellText;

	public Image panelImage;
	public Text panelText;

	GameObject cardGO;
	public void SetCardGO(GameObject go) { cardGO = go; } 
	CardAsset cardAsset;
	public void SetCardAsset(CardAsset asset) { cardAsset = asset; } 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowCardInfo(GameObject go)
	{
		SetCardGO(go);
		AddCardToDeck cardComponent = go.GetComponent<AddCardToDeck>();
		SetCardAsset(cardComponent.cardAsset);

		panelImage.sprite = cardAsset.CardImage;
		panelText.text = cardAsset.name;

		if (cardComponent.isOwned)
		{
			buyButton.gameObject.SetActive(false);
			sellButton.gameObject.SetActive(true);
			sellText.text = "+" + (cardAsset.cardCost/2 ).ToString();
		}
		else
		{
			sellButton.gameObject.SetActive(false);
			buyButton.gameObject.SetActive(true);
			buyText.text = "-" + cardAsset.cardCost.ToString();
		}

	}

	public void BuyCard()
	{
		ShopManager.Instance.BuyCard(cardGO);


	}

	public void SellCard()
	{
		ShopManager.Instance.SellCard(cardGO);
	}
}
