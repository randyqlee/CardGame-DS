using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class SkillHoverPreview: MonoBehaviour
{
    public GameObject previewGameObject;


    public GameObject previewGameObjectCreature;
	public List<GameObject> previewCreatureAbility = new List<GameObject>();
	public float TargetScaleCreature;

	Vector3 curPosition;
	Vector3 lastPosition;
 
    void OnMouseDown()
    {
        ShowPreview();

        curPosition = Input.mousePosition;
		lastPosition = curPosition;


    }

    void OnMouseUp()
    {
        HidePreview();
      

    }

    //prevent the preview card from being dragged when attacking
	void Update()
	{
		curPosition = Input.mousePosition;

		if (previewGameObject.activeSelf && gameObject.GetComponent<DraggingActions>().CanDrag)
		{
			if (curPosition != lastPosition)
			{
				previewGameObject.SetActive(false);
			}
		}
		lastPosition = curPosition;
	}


    
    // OTHER METHODS

//DS
    void ShowPreview(){

        previewGameObject.SetActive(true);
        previewGameObject.transform.position = new Vector3(0,1,0);

    }

    public void HidePreview()
    {
        previewGameObject.SetActive(false);

    }
   
}
