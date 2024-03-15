using UnityEngine;
using VContainer.Unity;

public sealed class PlayerEntity : Entity,
    IInitializable
{
    [SerializeField]
    private Player _player;

    public void Initialize()
    {
        Init();
    }

    private void Init()
    {
        AddComponentToEntity(new HPComponent(_player.hp));
        AddComponentToEntity(new BulletStorageComponent(_player.bulletStorage));
        AddComponentToEntity(new DeathComponent(_player.isDead));
        AddComponentToEntity(new TransformComponent(_player.transform));
        AddComponentToEntity(new ColliderComponent(_player.GetComponent<Collider>()));
        AddComponentToEntity(new OnDamageComponent(_player.OnDamage));
        AddComponentToEntity(new DestroyedComponent(_player.onDestroy));
    }
}