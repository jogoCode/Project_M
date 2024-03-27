using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    static public Action isActive;
    static public Action isNotActive;

    void Start()
    {
        isActive?.Invoke();
        isNotActive?.Invoke();
    }

    void Activate()
    {
        isActive();
    }


    void DesActivate()
    {
        isNotActive();
    }
}
