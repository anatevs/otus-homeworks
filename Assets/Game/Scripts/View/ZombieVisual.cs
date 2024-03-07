using UnityEngine;

public class ZombieVisual : MonoBehaviour
{
    [SerializeField]
    private Zombie _zombie;

    [SerializeField]
    private Animator _animator;

    private ZombieAnimationMechanic _zombieAnimationMechanic;

    private void Awake()
    {
        _zombieAnimationMechanic = new ZombieAnimationMechanic(_animator,
            _zombie.isAttacking,
            _zombie.OnDamageCounted,
            _zombie.moveDirection,
            _zombie.isDead);
    }

    private void Update()
    {
        _zombieAnimationMechanic.Update();
    }

    private void OnEnable()
    {
        _zombieAnimationMechanic.OnEnable();
    }

    private void OnDisable()
    {
        _zombieAnimationMechanic.OnDisable();
    }
}