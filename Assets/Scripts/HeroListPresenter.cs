using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroListPresenter : IDisposable
{
    public event Action<int, bool> OnSetActive;
    public event Action<int> OnDestroy;
    public event Action<int, int> OnChangeStats;

    public HeroEntityList HeroEntityList => _heroList;


    private readonly HeroEntityList _heroList;

    public HeroListPresenter(HeroEntityList heroList)
    {
        _heroList = heroList;
    }

    void IDisposable.Dispose()
    {

    }
}