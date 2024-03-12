using UnityEngine;

public sealed class PlayerEntity : Entity
{
    [SerializeField]
    private Player _player;

    private void Awake()
    {
        AddComponentToEntity(new HPComponent(_player.hp));
        AddComponentToEntity(new BulletStorageComponent(_player.bulletStorage));
        AddComponentToEntity(new DeathComponent(_player.isDead));
        AddComponentToEntity(new TransformComponent(_player.transform));
        AddComponentToEntity(new ColliderComponent(_player.GetComponent<Collider>()));
        AddComponentToEntity(new OnDamageComponent(_player.OnDamage));
    }
}