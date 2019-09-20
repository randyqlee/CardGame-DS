using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScriptToOpenOnePack : MonoBehaviour {
  
    public Image GlowImage;
    public Color32 GlowColor;
    private bool allowedToOpen = false;
    private Collider col;

    void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    public void AllowToOpenThisPack()
    {
        allowedToOpen = true;
        //DS ShopManager.Instance.OpeningArea.AllowedToDragAPack = false;
        // Disable back button so that player can not exit the pack opening screen while he has not opened a pack
        //DS ShopManager.Instance.OpeningArea.BackButton.interactable = false;
        if (CursorOverPack())
            GlowImage.DOColor(GlowColor, 0.5f);
        
        StartCoroutine(DancingPack());

        GlowImage.DOColor(GlowColor, 0.5f);
    }


    IEnumerator DancingPack()
    {
        while (true)
        {

            gameObject.transform.DOScale(1.3f,1f);
            gameObject.transform.DOLocalRotate(new Vector3(0f,0f,4f),3f);
            yield return new WaitForSeconds(1f);
            gameObject.transform.DOScale(1.5f,1f);
            gameObject.transform.DOLocalRotate(new Vector3(0f,0f,-4f),3f);
            yield return new WaitForSeconds(1f);

        }

        
    }

    private bool CursorOverPack()
    {
        RaycastHit[] hits;
        // raycst to mousePosition and store all the hits in the array
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 30f);

        bool passedThroughTableCollider = false;
        foreach (RaycastHit h in hits)
        {
            // check if the collider that we hit is the collider on this GameObject
            if (h.collider == col)
                passedThroughTableCollider = true;
        }
        return passedThroughTableCollider;
    }

    void OnMouseEnter()
    {
        if (allowedToOpen)
            GlowImage.DOColor(GlowColor, 0.5f);
    }

    void OnMouseExit()
    {
        // turn the glow Off
        GlowImage.DOColor(Color.clear, 0.5f);
    }

    void OnMouseDown()
    {
        if (allowedToOpen)
        {
            // 0) prevent from opening again
            allowedToOpen = false;
            // Start a dotween sequence
            Sequence s = DOTween.Sequence();
            // 1) raise the pack to opening position
           // s.Append(transform.DOLocalMoveZ(-2f, 0.5f));
            s.Append(transform.DOShakeRotation(1.5f, 20f, 20));

            s.OnComplete(() =>
                {
                    // 2) add glow, particle system

                    // 3): 
                    //DS ShopManager.Instance.OpeningArea.ShowPackOpening(transform.position);
                    //DS if (ShopManager.Instance.PacksCreated>0)
                    //DS     ShopManager.Instance.PacksCreated--;
                    // 4) destroy this pack in the end of the sequence
                    Destroy(gameObject);

                    //PackOpeningScreen.Instance.OpenPack();
                    PackOpen.Instance.OpenPack();           


                }); 
        }
    }
}
