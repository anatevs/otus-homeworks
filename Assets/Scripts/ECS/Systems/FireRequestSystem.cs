using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using VContainer;

public class FireRequestSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;
    private Filter _prefabsFilter;

    private PrefabObjectsTypes _arrow;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<FireRequest>()
            .With<Target>()
            .With<Team>()
            .Build();

        _prefabsFilter = this.World.Filter
            .With<Prefab>()
            .With<PrefabType>()
            .With<Team>()
            .Build();

        _arrow = PrefabObjectsTypes.Arrow;
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            entity.AddComponent<Standing>();

            foreach (Entity prefabEntity in _prefabsFilter)
            {
                if (prefabEntity.GetComponent<Team>().value == entity.GetComponent<Team>().value
                    && prefabEntity.GetComponent<PrefabType>().value == _arrow)
                {
                    GameObject newGameObject = GameObject.Instantiate(prefabEntity.GetComponent<Prefab>().prefab,
                        entity.GetComponent<TransformView>().value.position,
                        entity.GetComponent<Rotation>().value);

                    Entity newEntity = newGameObject.GetComponent<PositionProvider>().Entity;
                    newEntity.GetComponent<MoveDirection>() = entity.GetComponent<MoveDirection>();
                    if (newEntity.Has<Inactive>())
                    {
                        newEntity.RemoveComponent<Inactive>();
                    }
                    break;
                }
            }

            entity.RemoveComponent<FireRequest>();
        }
    }

    public void Dispose()
    {

    }
}