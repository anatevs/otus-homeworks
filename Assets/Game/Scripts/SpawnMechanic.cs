using UnityEngine;

public class SpawnMechanic
{
    private IAtomicEvent OnShoot;
    private GameObject _gameObject;
    private Vector3 _position;
    private Quaternion _rotation;

    public SpawnMechanic(IAtomicEvent onShoot, GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        OnShoot = onShoot;
        _gameObject = gameObject;
        _position = position;
        _rotation = rotation;
    }

    public void OnEnable()
    {
        OnShoot.Subscribe(Spawn);
    }

    public void OnDisable()
    {
        OnShoot.Unsubscribe(Spawn);
    }

    private void Spawn()
    {
        Object.Instantiate(_gameObject, _position, _rotation);
    }
}