﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HoverPreview: MonoBehaviour
{
    // PUBLIC FIELDS
    public GameObject TurnThisOffWhenPreviewing;  // if this is null, will not turn off anything 
    public Vector3 TargetPosition;
    public float TargetScale;
    public GameObject previewGameObject;

    //DS

    //public Vector3 originalPosition;

    public Vector3 TargetPositionCreature;
    public float TargetScaleCreature;
    public GameObject previewGameObjectCreature;
    public bool ActivateInAwake = false;

    // PRIVATE FIELDS
    private static HoverPreview currentlyViewing = null;

    // PROPERTIES WITH UNDERLYING PRIVATE FIELDS
    private static bool _PreviewsAllowed = true;
    public static bool PreviewsAllowed
    {
        get { return _PreviewsAllowed;}

        set 
        { 
            //Debug.Log("Hover Previews Allowed is now: " + value);
            _PreviewsAllowed= value;
            if (!_PreviewsAllowed)
                StopAllPreviews();
        }
    }

    private bool _thisPreviewEnabled = false;
    public bool ThisPreviewEnabled
    {
        get { return _thisPreviewEnabled;}

        set 
        { 
            _thisPreviewEnabled = value;
            if (!_thisPreviewEnabled)
                StopThisPreview();
        }
    }

    public bool OverCollider { get; set;}
 
    // MONOBEHVIOUR METHODS
    void Awake()
    {
        ThisPreviewEnabled = ActivateInAwake;
        //originalPosition = this.transform.localPosition;
    }
            
    void OnMouseEnter()
    {
        OverCollider = true;
        if (PreviewsAllowed && ThisPreviewEnabled)
            PreviewThisObject();
    }
        
    void OnMouseExit()
    {
        OverCollider = false;

        if (!PreviewingSomeCard())
            StopAllPreviews();
    }

    // OTHER METHODS
    void PreviewThisObject()
    {
        // 1) clone this card 
        // first disable the previous preview if there is one already
        StopAllPreviews();
        // 2) save this HoverPreview as curent
        currentlyViewing = this;
        // 3) enable Preview game object
        //previewGameObject.SetActive(true);
        // 4) disable if we have what to disable
        if (TurnThisOffWhenPreviewing!=null)
            TurnThisOffWhenPreviewing.SetActive(false); 
        // 5) tween to target position
        //previewGameObject.transform.localPosition = Vector3.zero;
        //previewGameObject.transform.localScale = Vector3.one;

        //previewGameObject.transform.DOLocalMove(TargetPosition, 1f).SetEase(Ease.OutQuint);
        //previewGameObject.transform.DOScale(TargetScale, 1f).SetEase(Ease.OutQuint);


//DS
        previewGameObjectCreature.transform.DOScale(TargetScaleCreature, 0.1f).SetEase(Ease.OutQuint);
    }

    void StopThisPreview()
    {
        
        previewGameObject.SetActive(false);
        previewGameObject.transform.localScale = Vector3.one;
        previewGameObject.transform.localPosition = Vector3.zero;

//DS
        if (previewGameObjectCreature != null){
        previewGameObjectCreature.transform.localScale = Vector3.one;
        previewGameObjectCreature.transform.DOScale(1f,0.1f).SetEase(Ease.OutQuint);
        }

        if (TurnThisOffWhenPreviewing!=null)
            TurnThisOffWhenPreviewing.SetActive(true); 
    }

    // STATIC METHODS
    private static void StopAllPreviews()
    {
        Debug.Log("Stop All Preview");
        if (currentlyViewing != null)
        {
            currentlyViewing.previewGameObject.SetActive(false);
            currentlyViewing.previewGameObject.transform.localScale = Vector3.one;
            currentlyViewing.previewGameObject.transform.localPosition = Vector3.zero;

//DS        
            if (currentlyViewing.previewGameObjectCreature != null){
            
            Debug.Log("Stop All Zoom");
            currentlyViewing.previewGameObjectCreature.transform.localScale = Vector3.one;
            }

            if (currentlyViewing.TurnThisOffWhenPreviewing!=null)
                currentlyViewing.TurnThisOffWhenPreviewing.SetActive(true); 
        }
         
    }

    private static bool PreviewingSomeCard()
    {
        if (!PreviewsAllowed)
            return false;

        HoverPreview[] allHoverBlowups = GameObject.FindObjectsOfType<HoverPreview>();

        foreach (HoverPreview hb in allHoverBlowups)
        {
            if (hb.OverCollider && hb.ThisPreviewEnabled)
                return true;
        }

        return false;
    }

   
}
