//DS

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CreatureHoverPreview: MonoBehaviour
{

	//Creature Card Preview (for enemy)
    public GameObject previewGameObject;

	//Creature Card self (for own creatures)
    public GameObject previewGameObjectCreature;
	public List<GameObject> previewCreatureAbility = new List<GameObject>();
	public float TargetScaleCreature;

	Vector3 curPosition;
	Vector3 lastPosition;

 
    // MONOBEHVIOUR METHODS
    void Awake()
    {

    }
            

//Ds

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

		if (GetComponentInParent<PlayerArea>().owner == AreaPosition.Low)
			PreviewThisObject();
		else
		{
			previewGameObject.SetActive(true);
			previewGameObject.transform.position = new Vector3(0,-3,0);
		}
        	

    }

    void HidePreview()
    {
		if (GetComponentInParent<PlayerArea>().owner == AreaPosition.Low)
			StopThisPreview();
		else
        	previewGameObject.SetActive(false);
	}


    // OTHER METHODS
    void PreviewThisObject()
    {
        previewGameObjectCreature.transform.DOScale(TargetScaleCreature, 0.01f).SetEase(Ease.OutQuint);
        
    }

	void StopThisPreview()
	{
		previewGameObjectCreature.transform.localScale = Vector3.one;
        previewGameObjectCreature.transform.DOScale(1f,0.01f).SetEase(Ease.OutQuint);
	}




}
