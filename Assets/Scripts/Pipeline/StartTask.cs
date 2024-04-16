using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTask : Task
{
    protected override void OnRun()
    {
        Debug.Log("Run start task");

        Finish();
    }

    protected override void OnFinished()
    {
        Debug.Log("start task is finished");
    }
}