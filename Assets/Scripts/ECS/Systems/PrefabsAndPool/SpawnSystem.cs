using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

public sealed class SpawnSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _spawnFilter;

    private readonly TeamService _teamService;

    public SpawnSystem(TeamService teamService)
    {
        _teamService = teamService;
    }

    public void OnAwake()
    {
        _spawnFilter = this.World.Filter
            .With<SpawnRequest>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _spawnFilter)
        {
            SpawnRequest spawnRequest = entity.GetComponent<SpawnRequest>();
            ActivateSpawned(spawnRequest.spawnGO, spawnRequest.worldTransform);

            entity.RemoveComponent<SpawnRequest>();
        }
    }

    private void ActivateSpawned(GameObject go, Transform worldTransform)
    {
        go.transform.SetParent(worldTransform);
        go.SetActive(true);

        Entity spawnEntity = go.GetComponent<UniversalProvider>().Entity;
        spawnEntity.RemoveComponent<Inactive>();

        _teamService.AddToTeam(spawnEntity);
    }

    public void Dispose()
    {
    }
}