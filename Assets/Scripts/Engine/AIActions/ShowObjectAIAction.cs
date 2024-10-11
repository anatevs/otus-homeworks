using Atomic.AI;
using System;
using UnityEngine;

[Serializable]
public class ShowObjectAIAction : IBlackboardAction
{
    [SerializeField]
    private bool _isShow;

    [SerializeField]
    private GameObject _objectToShow;

    public void Invoke(IBlackboard blackboard)
    {
        _objectToShow.SetActive(_isShow);
    }
}