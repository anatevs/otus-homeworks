using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainerExtensions;

public sealed class PipelineInstaller : MonoBehaviour
{
    private readonly Pipeline _pipeline = new();

    private IObjectResolver _objectResolver;

    [Inject]
    public void Constuct(IObjectResolver objResolver)
    {
        _objectResolver = objResolver;
    }

    private void Awake()
    {
        _pipeline.AddTask(new StartTask());
        _pipeline.AddTask(ObjectResolverExtension.ResolveInstance<TurnTask>(_objectResolver));
    }

    private void Start()
    {
        _pipeline.Run();
    }
}