using GameEngine;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadResources : SaveLoader<ResourcesParams, ResourceService>
{
    private readonly ResourceService _resourceService;
    private readonly OnSceneObjectsService _objectsOnScene;

    private readonly Dictionary<string, ScriptableObject> _defaultConfigs;

    public SaveLoadResources(ResourceService resourceService,  
        OnSceneObjectsService objectsOnScene, 
        Dictionary<string, ScriptableObject> defaultConfigs)
    {
        _resourceService = resourceService;
        _objectsOnScene = objectsOnScene;

        _defaultConfigs = defaultConfigs;
    }

    protected override void SetupParamsData(ResourcesParams resourcesParams)
    {
        LoadFromScene();
        IEnumerable<Resource> resources = _resourceService.GetResources();
        resourcesParams.SetParams(resources);
    }

    protected override void LoadDefault()
    {
        LoadFromScene();
    }

    protected override ResourcesParams ConvertDataToParams()
    {
        var resources = _resourceService.GetResources();

        ResourcesParams resourcesParams = new ResourcesParams();
        resourcesParams.SetupResources(resources);

        return resourcesParams;
    }

    private void LoadFromScene()
    {
        _resourceService.SetResources(_objectsOnScene.GetSceneObjects<Resource>());
    }
}