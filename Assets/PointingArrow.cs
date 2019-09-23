using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PointingArrow : MonoBehaviour
{

    public bool danceOn = true;
    // Start is called before the first frame update

    public float pointDistance = 5f;

    public float pointTime = 0.5f;

    void Awake()
    {
      


    }
    void Start()
    {


        if (gameObject.transform.eulerAngles.z == 90)
        {
            StartCoroutine(DanceUP());
        }

        if (gameObject.transform.eulerAngles.z == 180)
        {
            StartCoroutine(DanceLEFT());
        }
        if (gameObject.transform.eulerAngles.z == 270)
        {
            StartCoroutine(DanceDOWN());
        }

        if (gameObject.transform.eulerAngles.z == 0)
        {
            StartCoroutine(DanceRIGHT());
        }

        
        
    }

    IEnumerator DanceUP()
    {
        while (danceOn)
        {

            gameObject.transform.DOMoveY(gameObject.transform.position.y + pointDistance, pointTime);
            yield return new WaitForSeconds(pointTime);
            gameObject.transform.DOMoveY(gameObject.transform.position.y - pointDistance, pointTime);
            yield return new WaitForSeconds(pointTime);

        }
       
    }

    IEnumerator DanceDOWN()
    {
        while (danceOn)
        {

            gameObject.transform.DOMoveY(gameObject.transform.position.y - pointDistance, pointTime);
            yield return new WaitForSeconds(pointTime);
            gameObject.transform.DOMoveY(gameObject.transform.position.y + pointDistance, pointTime);
            yield return new WaitForSeconds(pointTime);

        }
       
    }

    IEnumerator DanceLEFT()
    {
        while (danceOn)
        {

            gameObject.transform.DOMoveX(gameObject.transform.position.x + pointDistance, pointTime);
            yield return new WaitForSeconds(pointTime);
            gameObject.transform.DOMoveX(gameObject.transform.position.x - pointDistance, pointTime);
            yield return new WaitForSeconds(pointTime);

        }
       
    }

    IEnumerator DanceRIGHT()
    {
        while (danceOn)
        {

            gameObject.transform.DOMoveX(gameObject.transform.position.x - pointDistance, pointTime);
            yield return new WaitForSeconds(pointTime);
            gameObject.transform.DOMoveX(gameObject.transform.position.x + pointDistance, pointTime);
            yield return new WaitForSeconds(pointTime);

        }
       
    }
}
