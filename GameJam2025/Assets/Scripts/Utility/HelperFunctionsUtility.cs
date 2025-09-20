using System;
using System.Collections;
using UnityEngine;

public static class HelperFunctionsUtility 
{
    public static IEnumerator IE_WaitForSeconds(Action callback, float time)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    public static IEnumerator IE_WaitForFrames(Action callback, int frames)
    {
        for (int i = 0; i < frames; i++)
            yield return null;
        
        callback?.Invoke();
    }
}
