using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEntityList
{
    private int _currentActive;
    private int _nextActive;
    private List<int> _removedIndexes = new List<int>();


    private readonly List<HeroEntity> _list;

    public HeroEntityList(List<HeroEntity> list)
    {
        _list = list;
    }

    public int OnNextMove()
    {
        _currentActive = _nextActive;
        SetNextIndex();

        return _currentActive;
    }

    public void OnRemove(HeroEntity entity)
    {
        int removedIdx = _list.IndexOf(entity);
        _list.RemoveAt(removedIdx);
        UpdateIndexes(removedIdx);
    }

    private void SetNextIndex()
    {
        int nextUnchecked = (_currentActive + 1) % _list.Count;
        if (_removedIndexes.Count > 0)
        {
            for (int i = 0; i < _removedIndexes.Count; i++)
            {
                if (_removedIndexes[i] == nextUnchecked)
                {
                    nextUnchecked = (nextUnchecked + 1) % _list.Count;
                }
                else
                {
                    break;
                }
            }
        }

        _nextActive = nextUnchecked;
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

    public HeroEntity Get(int index)
    {
        return _list[index];
    }

    public int GetCurrentActiveIndex()
    {
        return _currentActive;
    }

    public HeroEntity GetCurrentActive()
    {
        return _list[_currentActive];
    }
}