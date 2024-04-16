using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PipelineInstaller : MonoBehaviour
{
    private readonly Pipeline _pipeline = new();

    private void Awake()
    {
        _pipeline.AddTask(new StartTask());
        _pipeline.AddTask(new TurnTask());
    }

    private void Start()
    {
        _pipeline.Run();
    }
}