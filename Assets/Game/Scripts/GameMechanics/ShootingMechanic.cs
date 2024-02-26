using UnityEngine;

public class ShootingMechanic
{
    private readonly IAtomicEvent OnShoot;
    private readonly IAtomicVariable<int> _weaponMagazine;
    private readonly int _shootAmount;

    public ShootingMechanic(IAtomicEvent OnShoot, IAtomicVariable<int> weaponMagazine)
    {
        this.OnShoot = OnShoot;
        _weaponMagazine = weaponMagazine;
        _shootAmount = 1;
    }

    public void OnEnable()
    {
        OnShoot.Subscribe(MakeShoot);
    }

    public void OnDisable()
    {
        OnShoot.Unsubscribe(MakeShoot);
    }

    private void MakeShoot()
    {
        if (_weaponMagazine.Value <= 0)
        {
            return;
        }
        else
        {
            _weaponMagazine.Value -= _shootAmount;
            Debug.Log("Shoot!");
        }
    }
}
