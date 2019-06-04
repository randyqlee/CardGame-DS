using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffPanel : MonoBehaviour {

	public GameObject buffImage;
	// Use this for initialization
	public void AddBuffItem(BuffEffect buffEffect)
	{
		GameObject go = Instantiate(buffImage);
		go.transform.SetParent(this.transform);
		go.transform.localScale = new Vector3(1,1,1);
		IDHolder id = go.AddComponent<IDHolder>();
        id.UniqueID = buffEffect.buffID;
		go.GetComponent<Image>().sprite = buffEffect.buffIcon;
		go.GetComponentInChildren<Text>().text = buffEffect.buffCooldown.ToString();
	}

}
