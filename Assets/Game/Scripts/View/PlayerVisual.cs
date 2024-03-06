using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private Animator _animator;

    private PlayerAnimatorMechanic _animatorController;

    private void Awake()
    {
        _animatorController = new PlayerAnimatorMechanic(_animator, 
            _player.moveDirection, _player.isDead,
            _player.OnDamage, _player.FireRequest);
    }

    private void Update()
    {
        _animatorController.Update();
    }

    private void OnEnable()
    {
        _animatorController.OnEnable();
    }

    private void OnDisable()
    {
        _animatorController.OnDisable();
    }
}