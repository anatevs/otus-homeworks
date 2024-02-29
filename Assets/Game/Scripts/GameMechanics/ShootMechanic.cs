using UnityEngine;

public class ShootMechanic
{
    private readonly IAtomicEvent _shootEvent;
    private readonly Bullet _bullet;
    private readonly Transform _shootPoint;
    private readonly Transform _player;

    public ShootMechanic(IAtomicEvent shootEvent, Bullet bullet, Transform shootPoint, Transform player)
    {
        _shootEvent = shootEvent;
        _bullet = bullet;
        _shootPoint = shootPoint;
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
        bullet.moveDirection.Value = _player.forward;
    }
}