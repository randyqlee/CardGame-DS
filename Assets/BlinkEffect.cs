using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEffect : MonoBehaviour
{

    public bool blinkOn = true;
    // Start is called before the first frame update

    public GameObject objectToBlink;
    void Start()
    {
        StartCoroutine(Blink());        
    }

    void OnEnable()
    {
        
    }

    // Update is called once per frame

    IEnumerator Blink()
    {
        while (blinkOn)
        {

            objectToBlink.SetActive(true);
            yield return new WaitForSeconds(0.5f);

            objectToBlink.SetActive(false);
            yield return new WaitForSeconds(0.5f);


        }

        
    }
}
