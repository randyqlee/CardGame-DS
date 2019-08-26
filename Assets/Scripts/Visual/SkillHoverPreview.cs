using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class SkillHoverPreview: MonoBehaviour
{
    public GameObject previewGameObject;

 
    void OnMouseDown()
    {
        ShowPreview();
      

    }

    void OnMouseUp()
    {
        HidePreview();
      

    }

    // OTHER METHODS

//DS
    void ShowPreview(){

        previewGameObject.SetActive(true);
        previewGameObject.transform.position = new Vector3(0,1,0);

    }

    void HidePreview()
    {
        previewGameObject.SetActive(false);

    }
   
}
