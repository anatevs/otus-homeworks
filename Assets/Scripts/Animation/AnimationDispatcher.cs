using System;
using UnityEngine;

public class AnimationDispatcher : MonoBehaviour
{
    public event Action<string> OnEventReceived;

    private void ReceiveEvent(string eventName)
    {
        Debug.Log(eventName);
        OnEventReceived.Invoke(eventName);
    }
}