using UnityEngine;

public class EntityProvider : MonoBehaviour
{
    public Entity Entity
    {
        get => _entity;
        private set => _entity = value;
    }

    private Entity _entity;

    public void Init(Entity entity)
    {
        _entity = entity;
    }
}