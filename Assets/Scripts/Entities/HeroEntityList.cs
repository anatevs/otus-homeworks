using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEntityList
{
    private int _currentActive;
    private int _nextActive;

    private List<HeroEntity> _list;

    public HeroEntityList(List<HeroEntity> list)
    {
        _list = list;
    }

    private int OnNextMove()
    {
        _currentActive = _nextActive;
        SetNextIndex();

        return _currentActive;
    }

    private void OnRemove(HeroEntity entity)
    {
        int removedIdx = _list.IndexOf(entity);
        _list.RemoveAt(removedIdx);
        UpdateIndexes(removedIdx);
    }

    private void SetNextIndex()
    {
        _nextActive = (_currentActive + 1) % _list.Count;
    }

    private void UpdateIndexes(int removedIndex)
    {
        if (removedIndex <= _currentActive)
        {
            _nextActive = _currentActive;
            _currentActive--;
        }
        else
        {
            SetNextIndex();
        }
    }
}