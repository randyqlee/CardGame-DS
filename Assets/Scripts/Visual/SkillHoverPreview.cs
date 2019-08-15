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

    }

    void HidePreview()
    {
        previewGameObject.SetActive(false);

    }
   
}
