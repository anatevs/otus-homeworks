using System.Collections.Generic;
using UnityEngine;

public class HeroEntityList
{
    private int _currentActive = -1;
    private int _nextActive = 0;
    private readonly List<int> _removedIndexes = new List<int>();
    private readonly List<int> _validIndexes = new List<int>();

    private readonly List<HeroEntity> _list;

    public HeroEntityList(List<HeroEntity> list)
    {
        _list = list;

        for (int i = 0; i < list.Count; i++)
        {
            _validIndexes.Add(i);
        }
    }

    public void OnNextMove()
    {
        SetNextIndex();
        _currentActive = _nextActive;
    }

    public void OnRemove(HeroEntity entity)
    {
        int removedIdx = _list.IndexOf(entity);
        //_removedIndexes.Add(removedIdx);
        //_removedIndexes.Sort();

        _validIndexes.Remove(removedIdx);
    }

    private void SetNextIndex()
    {
        int nextUnchecked = CalcNextIndex(_currentActive);

        if (_validIndexes.Count < _list.Count)
        {
            nextUnchecked = FindNextValid(nextUnchecked);
        }

        _nextActive = nextUnchecked;
    }

    //private int FindNextValid(int nextUnchecked)
    //{
    //    for (int i = 0; i < _removedIndexes.Count; i++)
    //    {
    //        if (_removedIndexes[i] == nextUnchecked)
    //        {
    //            nextUnchecked = CalcNextIndex(nextUnchecked);
    //            return FindNextValid(nextUnchecked);
    //        }
    //    }

    //    return nextUnchecked;
    //}


    private int FindNextValid(int nextUnchecked)
    {
        for (int i = 0; i < _validIndexes.Count; i++)
        {
            if (_validIndexes[i] == nextUnchecked)
            {
                return nextUnchecked;
            }
        }

        nextUnchecked = CalcNextIndex(nextUnchecked);
        return FindNextValid(nextUnchecked);
    }

    private int CalcNextIndex(int prev)
    {
        return (prev + 1) % _list.Count;
    }

    public IReadOnlyList<int> GetValidIndexes()
    {
        return _validIndexes;
    }

    public HeroEntity Get(int index)
    {
        return _list[index];
    }

    public HeroEntity GetCurrentActive()
    {
        return _list[_currentActive];
    }
}