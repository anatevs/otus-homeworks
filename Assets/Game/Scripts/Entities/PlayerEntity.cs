using UnityEngine;

public class PlayerEntity : Entity
{
    [SerializeField]
    private Player _player;

    private void Awake()
    {
        AddComponentToEntity(new HPComponent(_player.hp));
        AddComponentToEntity(new BulletStorageComponent(_player.bulletStorage));
    }
}