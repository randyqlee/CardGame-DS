﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopupManager : MonoBehaviour
{
    public static TutorialPopupManager Instance;
    public List<GameObject> popupMessages;
    // Start is called before the first frame update
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
}
