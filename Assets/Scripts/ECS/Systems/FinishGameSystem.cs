using Scellecs.Morpeh;
using UnityEngine;

public class FinishGameSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _basesFilter;
    private Filter _gameObjectsFilter;
    private Stash<FinishGameRequest> _finishStash;

    public void OnAwake()
    {
        _basesFilter = this.World.Filter
            .With<BaseFlag>()
            .Without<Inactive>()
            .Build();

        _gameObjectsFilter = this.World.Filter
            .With<Team>()
            .Build();

        _finishStash = this.World.GetStash<FinishGameRequest>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _basesFilter)
        {
            if (_finishStash.Has(entity))
            {
                foreach (Entity gameEntity in _gameObjectsFilter)
                {
                    gameEntity.SetComponent<Inactive>(new Inactive());
                }

                Debug.Log($"Game is over, {(TeamType) (((int) entity.GetComponent<Team>().value + 1)%2)} is win");
            }
        }
    }

    public void Dispose()
    {
    }
}