using System;
using UnityEngine;

public abstract class Task
{
    private event Action Callback;

    public void Run(Action FinishCallback)
    {
        Callback = FinishCallback;
        OnRun();
    }

    protected abstract void OnRun();

    public void Finish()
    {
        if (Callback != null)
        {
            Action CachedCallback = Callback;
            CachedCallback.Invoke();
            Callback = null;
        }

        OnFinished();
    }

    protected virtual void OnFinished()
    {
    }
}