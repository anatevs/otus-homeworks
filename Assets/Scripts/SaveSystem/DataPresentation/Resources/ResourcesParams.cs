using GameEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class ResourcesParams
{
    [JsonProperty]
    private Dictionary<string, int> dict = new Dictionary<string, int>();

    public void SetupResources(IEnumerable<Resource> resources)
    {
        foreach (Resource resource in resources)
        {
            AddResource(resource);
        }
    }

    private void AddResource(Resource resource)
    {
        dict.Add(resource.ID, resource.Amount);
    }

    public void SetParams(IEnumerable<Resource> resources)
    {
        foreach(Resource resource in resources)
        {
            resource.Amount = dict[resource.ID];
        }
    }
}