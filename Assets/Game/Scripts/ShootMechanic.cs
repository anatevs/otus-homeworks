using UnityEngine;

public class ShootMechanic
{
    private IAtomicEvent _shootEvent;
    private Bullet _bullet;
    private Transform _shootPoint;
    private Transform _player;

    public ShootMechanic(IAtomicEvent shootEvent, Bullet bullet, Transform position, Transform player)
    {
        _shootEvent = shootEvent;
        _bullet = bullet;
        _shootPoint = position;
        _player = player;
    }

    public void OnEnable()
    {
        _shootEvent.Subscribe(Spawn);
    }

    public void OnDisable()
    {
        _shootEvent.Unsubscribe(Spawn);
    }

    private void Spawn()
    {
        var bullet = Object.Instantiate(_bullet, _shootPoint.position, _player.rotation);
    }
}