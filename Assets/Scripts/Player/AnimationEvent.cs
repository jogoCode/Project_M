using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    static public Action isActive;


    void Activate()
    {
        isActive();
    }

}
