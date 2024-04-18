using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using VContainerExt;

public sealed class PipelineInstaller : IInitializable, IDisposable
{
    private readonly TurnPipeline _turnPipeline;

    private readonly IObjectResolver _objectResolver;

    public PipelineInstaller(TurnPipeline turnPipeline, IObjectResolver objResolver)
    {
        _turnPipeline = turnPipeline;
        _objectResolver = objResolver;
    }

    void IInitializable.Initialize()
    {
        _turnPipeline.AddTask(new StartTask());
        _turnPipeline.AddTask(ObjectResolverExtension.ResolveInstance<TurnTask>(_objectResolver));
    }

    void IDisposable.Dispose()
    {
        _turnPipeline.Clear();
        _objectResolver?.Dispose();
    }
}