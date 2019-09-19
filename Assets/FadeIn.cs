using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class FadeIn : MonoBehaviour
{
    
    public Vector3 punchScale = new Vector3(0.1f,0.1f,0f);
    public float punchTime = 0.5f;
    public int punchVibrations = 1;
    public float punchElasticity = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        gameObject.transform.DOPunchScale(punchScale, punchTime, punchVibrations, punchElasticity);

    }
}
