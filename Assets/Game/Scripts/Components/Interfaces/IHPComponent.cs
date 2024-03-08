using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHPComponent
{
    public event Action<int> OnHPChanged;

    public int GetHP();
}