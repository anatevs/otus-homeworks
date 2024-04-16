using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTask : Task
{
    protected override void OnRun()
    {
        Debug.Log("Run turn task");

        Finish();
    }

    protected override void OnFinished()
    {
        Debug.Log("turn task is finished");
    }
}