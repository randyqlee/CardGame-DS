using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardToCollection : MonoBehaviour {

   	public CardAsset cardAsset;
    public void SetCardAsset(CardAsset asset) { cardAsset = asset; } 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void AddToCollection()
	{
		
        if (cardAsset == null)
            return;

        ShopManager.Instance.BuyCard(gameObject);

	}
}
