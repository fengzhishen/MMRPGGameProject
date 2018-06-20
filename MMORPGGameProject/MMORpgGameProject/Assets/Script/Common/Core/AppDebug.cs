using UnityEngine;
using System.Collections;

public sealed class AppDebug
{

    public static void Log(object message)
    {
#if DEBUG_MODEL
        Debug.Log(message);
#endif
    }

    public static void LogError(object message)
    {
#if DEBUG_MODEL
        Debug.LogError(message);
#endif
    }
}
