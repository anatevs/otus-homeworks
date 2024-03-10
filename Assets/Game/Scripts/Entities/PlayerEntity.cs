using UnityEngine;

public class PlayerEntity : Entity
{
    [SerializeField]
    private Player _player;

    private void Awake()
    {
        AddComponentToEntity(new HPComponent(_player.hp));
    }
}