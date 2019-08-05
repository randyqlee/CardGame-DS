using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class HoverPreview: MonoBehaviour
{
    // PUBLIC FIELDS
    public GameObject TurnThisOffWhenPreviewing;  // if this is null, will not turn off anything 
    public Vector3 TargetPosition;
    public float TargetScale;
    public GameObject previewGameObject;

    bool hoverDuration;

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
            //StartCoroutine(PreviewThisObject1());

         
//DS
        //ShowAbilityPreview();
    }
        
    void OnMouseExit()
    {
        OverCollider = false;

        if (!PreviewingSomeCard())        
            StopAllPreviews();
          
        
            

//DS
        
        //HideAbilityPreview();
        //GetComponentInParent<PlayerArea>().abilityPreview.gameObject.SetActive(false);
        
    
    }


//Ds

    void OnMouseDown()
    {
        OverCollider = true;
        if (PreviewsAllowed && ThisPreviewEnabled)
        {
            PreviewThisObject();
            //ShowAbilityPreview();
        }
        else
        {
            HideAbilityPreview();
            //GetComponentInParent<PlayerArea>().abilityPreview.gameObject.SetActive(false);
        }

        

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
        previewGameObjectCreature.transform.DOScale(TargetScaleCreature, 0.01f).SetEase(Ease.OutQuint);

        
    }

     IEnumerator PreviewThisObject1()
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
       
        yield return new WaitForSeconds(2f);        
       
        previewGameObjectCreature.transform.DOScale(TargetScaleCreature, 0.01f).SetEase(Ease.OutQuint);

        yield return null;

        
    }//PreviewThisObject1


//DS
    void ShowAbilityPreview(){
/*
        int i = previewGameObjectCreature.GetComponent<OneCreatureManager>().abilityEffectSprite.Count;
        //GetComponentInParent<PlayerArea>().abilityPreview.gameObject.SetActive(true);
        
        for (int j = 0; j<i; j++)
        {
            GetComponentInParent<PlayerArea>().abilityPreview.ability[j].sprite = previewGameObjectCreature.GetComponent<OneCreatureManager>().abilityEffectSprite[j];
            GetComponentInParent<PlayerArea>().abilityPreview.ability[j].preserveAspect = false;
            var tempColor = GetComponentInParent<PlayerArea>().abilityPreview.ability[j].color;
            tempColor.a = 1f;
            GetComponentInParent<PlayerArea>().abilityPreview.ability[j].color = tempColor;

        }

*/
// get the Logic from the Visual ID, then get the Effects, and put in abilityPreview using Instantiate (AbilityCard)
        GetComponentInParent<PlayerArea>().abilityPreview.gameObject.SetActive(true);
        int ID = previewGameObjectCreature.GetComponent<IDHolder>().UniqueID;
        CreatureLogic cl = CreatureLogic.CreaturesCreatedThisGame[ID];

        //clear existing cards in panel
        if (GetComponentInParent<PlayerArea>().abilityPreview.abilities.GetComponentsInChildren<AbilityCard>()!=null)
        {
            for (int i = GetComponentInParent<PlayerArea>().abilityPreview.abilities.GetComponentsInChildren<AbilityCard>().Length - 1; i >= 0; i-- )
            {               
                Destroy(GetComponentInParent<PlayerArea>().abilityPreview.abilities.GetComponentsInChildren<AbilityCard>()[i].gameObject);
            }
        }

        //create new cards in panel
        foreach (CreatureEffect ce in cl.creatureEffects)
        {
             GameObject ac = GameObject.Instantiate (GlobalSettings.Instance.AbilityPreviewPrefab, GetComponentInParent<PlayerArea>().abilityPreview.abilities.transform) as GameObject;
             if (ce.abilityPreviewSprite!=null)
             ac.GetComponent<AbilityCard>().abilityImage.sprite = ce.abilityPreviewSprite;            
             ac.GetComponent<AbilityCard>().abilityCooldownText.text = ce.remainingCooldown.ToString();
            
        }
        




        
        //GetComponentInParent<PlayerArea>().abilityPreview.ability[0].sprite = previewGameObjectCreature.GetComponent<OneCreatureManager>().CreatureGraphicImage.sprite;
    }

    void StopThisPreview()
    {
        
        previewGameObject.SetActive(false);
        previewGameObject.transform.localScale = Vector3.one;
        previewGameObject.transform.localPosition = Vector3.zero;

//DS
        if (previewGameObjectCreature != null){
        previewGameObjectCreature.transform.localScale = Vector3.one;
        previewGameObjectCreature.transform.DOScale(1f,0.01f).SetEase(Ease.OutQuint);
        }

        if (TurnThisOffWhenPreviewing!=null)
            TurnThisOffWhenPreviewing.SetActive(true); 
    }

    // STATIC METHODS
    public static void StopAllPreviews()
    {
        //Debug.Log("Stop All Preview");
        if (currentlyViewing != null)
        {
            currentlyViewing.previewGameObject.SetActive(false);
            currentlyViewing.previewGameObject.transform.localScale = Vector3.one;
            currentlyViewing.previewGameObject.transform.localPosition = Vector3.zero;

//DS        
            if (currentlyViewing.previewGameObjectCreature != null){
            
            //Debug.Log("Stop All Zoom");
            currentlyViewing.previewGameObjectCreature.transform.localScale = Vector3.one;
            }

            if (currentlyViewing.TurnThisOffWhenPreviewing!=null)
                currentlyViewing.TurnThisOffWhenPreviewing.SetActive(true); 
        }

        

        
        

    }

    void HideAbilityPreview()
    {
        GameObject go = GetComponentInParent<PlayerArea>().abilityPreview.abilities;
        AbilityCard[] abilities = go.GetComponentsInChildren<AbilityCard>();
        if (abilities != null)
        {
            for (int i = abilities.Length - 1; i >= 0; i--)
            {
                GameObject.Destroy(abilities[i].gameObject);
            }
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
