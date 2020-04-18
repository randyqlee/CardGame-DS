using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public static SkillSystem Instance;

    void Awake()
    {
        Instance = this;
    }


    
}
