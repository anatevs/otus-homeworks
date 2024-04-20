using System.Collections.Generic;
using UnityEngine;

public class HeroEntityList
{
    private int _currentActive = -1;
    private int _nextActive = 0;
    private readonly List<int> _removedIndexes = new List<int>();

    private readonly List<HeroEntity> _list;

    public HeroEntityList(List<HeroEntity> list)
    {
        _list = list;
    }

    public void OnNextMove()
    {
        SetNextIndex();
        _currentActive = _nextActive;
    }

    public void OnRemove(HeroEntity entity)
    {
        int removedIdx = _list.IndexOf(entity);
        _removedIndexes.Add(removedIdx);
        _removedIndexes.Sort();
    }

    private void SetNextIndex()
    {
        int nextUnchecked = CalcNextIndex(_currentActive);

        if (_removedIndexes.Count > 0)
        {
            for (int i = 0; i < _removedIndexes.Count; i++)
            {
                if (_removedIndexes[i] == nextUnchecked)
                {
                    nextUnchecked = CalcNextIndex(nextUnchecked);
                }
            }
        }
        _nextActive = nextUnchecked;
    }

    private int CalcNextIndex(int prev)
    {
        return (prev + 1) % _list.Count;
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