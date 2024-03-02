using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private Animator _animator;

    private PlayerAnimatorController _animatorController;

    private void Awake()
    {
        _animatorController = new PlayerAnimatorController(_player.moveDirection, _player.isDead, _animator, _player.OnDamage, _player.ShootEvent);
    }

    private void Update()
    {
        _animatorController.Update();
    }
}