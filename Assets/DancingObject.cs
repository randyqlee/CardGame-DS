using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DancingObject : MonoBehaviour
{
    public bool danceOn = true;
    void Start()
    {

        StartCoroutine(Dance());
        
    }

    IEnumerator Dance()
    {
        while (danceOn)
        {

            gameObject.transform.DOScale(1.3f,1f);
            gameObject.transform.DOLocalRotate(new Vector3(0f,0f,4f),3f);
            yield return new WaitForSeconds(1f);
            gameObject.transform.DOScale(1.5f,1f);
            gameObject.transform.DOLocalRotate(new Vector3(0f,0f,-4f),3f);
            yield return new WaitForSeconds(1f);

        }

        
    }
}
