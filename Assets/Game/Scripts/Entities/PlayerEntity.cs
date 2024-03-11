using UnityEngine;
using VContainer;

public class PlayerEntity : Entity
{
    [SerializeField]
    private Player _player;

    private void Awake()
    {
        Debug.Log("entity init");
        AddComponentToEntity(new HPComponent(_player.hp));
        AddComponentToEntity(new BulletStorageComponent(_player.bulletStorage));
    }
}