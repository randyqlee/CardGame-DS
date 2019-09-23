using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToExit : MonoBehaviour
{
    public static TapToExit Instance;

    public GameObject target;
    public GameObject text;

    public bool isTapped = false;
    
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ListenForTap(bool textON = false)
    {
        target.SetActive(true);

        text.gameObject.SetActive(false);

        if (textON)
        {
            text.gameObject.SetActive(true);
        }
        
        do
        {
            yield return null;

        }
        while (!isTapped);

        target.SetActive(false);
        StopAllCoroutines();
        yield return null;
        isTapped = false;

    }


//put this on another script


}
