using System;
using UnityEngine;

public interface IUnspawnComponent
{
    public event Action<GameObject> OnUnspawn;
}