using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events 
{
    public static event Action <float,float> StartShake;
    public static void BroadcastStartShake(float shakingTime, float shakeMagnitude)
    {
        StartShake?.Invoke(shakingTime, shakeMagnitude);
    }
}
