using System;
using System.Linq;
using UnityEngine;

public class VFXDispatcher : MonoBehaviour
{
    [SerializeField]
    private VFXEvents[] _events;

    ParticleSystem _particleSystem;

    public event Action<VFXEventNames> OnReceiveEvent;

    private int _currIndex = 0;


    public void Init(ParticleSystem particleSystem)
    {
        _particleSystem = particleSystem;

        _events.OrderBy(x => x.time).ToArray();
    }

    private void Update()
    {
        if (_currIndex >= _events.Length)
        {
            return;
        }
        if (_particleSystem.time >= _events[_currIndex].time)
        {
            OnReceiveEvent.Invoke(_events[_currIndex].eventName);
            _currIndex++;
        }
    }
}