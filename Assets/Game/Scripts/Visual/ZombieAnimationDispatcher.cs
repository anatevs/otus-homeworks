using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimationDispatcher : MonoBehaviour
{
    [SerializeField]
    private Zombie _zombie;

    private const string HIT = "Hit";
    private const string DESTROY = "Destroy";

    public void RecieveEvent(string eventType)
    {
        if (eventType == HIT)
        {
            if (_zombie.isAttacking.Value)
            {
                _zombie.MakeDamage.Invoke();
            }
        }

        if(eventType == DESTROY)
        {
            _zombie.isDeactivated.Value = true;
        }

    }
}
