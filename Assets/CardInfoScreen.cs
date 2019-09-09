using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardInfoScreen : MonoBehaviour {

	public Button closeButton;
	public Button buyButton;
	public Text buyText;
	public Button sellButton;
	public Text sellText;

	public Image panelImage;
	public Text panelText;
	public Material greyScaleMaterial;

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

		if (!cardComponent.isAdded)
		{

			if (cardComponent.isOwned)
			{
				if (cardAsset.Rarity == RarityOptions.Common)
				{
					buyButton.gameObject.SetActive(false);
					sellButton.gameObject.SetActive(false);
					panelImage.material = null;

				}
				else
				{
					buyButton.gameObject.SetActive(false);
					sellButton.gameObject.SetActive(true);
					sellText.text = "+" + (cardAsset.cardCost/2 ).ToString();

					panelImage.material = null;

				}
				
			}
			else
			{
				sellButton.gameObject.SetActive(false);
				buyButton.gameObject.SetActive(true);
				buyText.text = "-" + cardAsset.cardCost.ToString();

				panelImage.material = greyScaleMaterial;
			}
		}

		else
		{
			buyButton.gameObject.SetActive(false);
			sellButton.gameObject.SetActive(false);
			panelImage.material = null;
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
