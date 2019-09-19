using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PointingHand : MonoBehaviour
{
    public bool danceOn = true;
    // Start is called before the first frame update

    public float pointDistance = 100f;

    public float pointTime = 1f;

    Vector3 initPosition;

    void Awake()
    {
      


    }
    void Start()
    {
            initPosition = gameObject.transform.position;

            StartCoroutine(DanceUP());
        
        
    }

    IEnumerator DanceUP()
    {
        while (danceOn)
        {

            gameObject.transform.DOMoveY(gameObject.transform.position.y + pointDistance, pointTime);
            yield return new WaitForSeconds(pointTime);
            gameObject.transform.position = initPosition;
            yield return null;
        }
       
    }
}
