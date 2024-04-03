using System;
using UnityEngine;

public class TriggerEnterListener : MonoBehaviour
{
    public event Action<Collider> TriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterEvent.Invoke(other);
    }
}