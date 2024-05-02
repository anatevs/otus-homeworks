using GameEngine;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class SaveLoadResources : SaveLoader<ResourcesParams, ResourceService>
{
    protected override void SetupParamsData(ResourcesParams resourcesParams, IObjectResolver context)
    {
        ResourceService resourceService = context.Resolve<ResourceService>();

        LoadFromScene(context);
        IEnumerable<Resource> resources = resourceService.GetResources();
        resourcesParams.SetParams(resources);
    }

    protected override void LoadDefault(IObjectResolver context)
    {
        LoadFromScene(context);
    }

    protected override ResourcesParams ConvertDataToParams(ResourceService resourceService)
    {
        var resources = resourceService.GetResources();

        ResourcesParams resourcesParams = new ResourcesParams();
        resourcesParams.SetupResources(resources);

        return resourcesParams;
    }

    private void LoadFromScene(IObjectResolver context)
    {
        ResourceService resourceService = context.Resolve<ResourceService>();
        SceneObjectsService objectsOnScene = context.Resolve<SceneObjectsService>();

        resourceService.SetResources(objectsOnScene.GetSceneObjects<Resource>());
    }
}