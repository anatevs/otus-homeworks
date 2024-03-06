using UnityEngine;

public class ZombieVisual : MonoBehaviour
{
    [SerializeField]
    private Zombie _zombie;

    [SerializeField]
    private Animator _animator;

    private ZombieVisualMechanic _zombieVisualMechanic;

    private void Awake()
    {
        _zombieVisualMechanic = new ZombieVisualMechanic(_animator,
            _zombie.OnDamageCounted,
            _zombie.moveDirection,
            _zombie.isDead);
    }

    private void Update()
    {
        _zombieVisualMechanic.Update();
    }

    private void OnEnable()
    {
        _zombieVisualMechanic.OnEnable();
    }

    private void OnDisable()
    {
        _zombieVisualMechanic.OnDisable();
    }
}