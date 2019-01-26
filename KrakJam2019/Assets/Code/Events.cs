using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events 
{
    public static event Action <float> TakeResourcesAfterConstract;
    public static void BroadcastTakeResourcesAfterConstract(float shakingTime)
    {
        TakeResourcesAfterConstract?.Invoke(shakingTime);
    }
}
