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

    private ObjectsTypeNames _arrow;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<FireRequest>()
            .With<Weapon>()
            .With<Team>()
            .Build();

        _prefabsFilter = this.World.Filter
            .With<Prefab>()
            .With<ObjectType>()
            .With<Team>()
            .Build();

        _arrow = ObjectsTypeNames.Arrow;
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (Entity entity in _filter)
        {
            entity.AddComponent<Standing>();

            Weapon weapon = entity.GetComponent<Weapon>();

            //start fire animation...
            entity.AddComponent<SpawnRequest>() = new SpawnRequest()
            {
                type = weapon.projectileType,
                team = entity.GetComponent<Team>().value,
                transform = weapon.firePoint
            };

            //foreach (Entity prefabEntity in _prefabsFilter)
            //{
            //    if (prefabEntity.GetComponent<Team>().value == entity.GetComponent<Team>().value
            //        && prefabEntity.GetComponent<ObjectType>().value == _arrow)
            //    {
            //        GameObject newGameObject = GameObject.Instantiate(prefabEntity.GetComponent<Prefab>().prefab,
            //            entity.GetComponent<TransformView>().value.position,
            //            entity.GetComponent<Rotation>().value);


            //        Entity newEntity = newGameObject.GetComponent<MovableProvider>().Entity;
            //        if (newEntity.Has<Inactive>())
            //        {
            //            newEntity.RemoveComponent<Inactive>();
            //        }
            //        break;
            //    }
            //}

            entity.RemoveComponent<FireRequest>();
        }
    }

    public void Dispose()
    {
    }
}