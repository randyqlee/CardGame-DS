using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffPanel : MonoBehaviour {

	public GameObject buffImage;
	// Use this for initialization
	public void AddBuffItem(BuffAsset buff)
	{
		GameObject go = Instantiate(buffImage);
		go.transform.SetParent(this.transform);


	}
}
