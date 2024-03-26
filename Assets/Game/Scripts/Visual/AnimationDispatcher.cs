using System;
using UnityEngine;
using UnityEngine.Events;

public class AnimationDispatcher : MonoBehaviour
{
    public event Action<string> OnEventRecieved;

    //[SerializeField]
    //private AnimationDispatcherEvent[] events;

    private void RecieveEvent(string otherEvent)
    {
        //for (int i = 0; i < events.Length; i++)
        //{
        //    if (string.Equals(events[i].EventName, otherEvent, StringComparison.InvariantCulture))
        //    {
        //        events[i].OnRecieved.Invoke();
        //    }
        //}
        OnEventRecieved?.Invoke(otherEvent);
    }


    //[Serializable]
    //private struct AnimationDispatcherEvent
    //{
    //    [field: SerializeField]
    //    public string EventName { get; private set; }

    //    [field: SerializeField]
    //    public UnityEvent OnRecieved { get; private set; }
    //}
}