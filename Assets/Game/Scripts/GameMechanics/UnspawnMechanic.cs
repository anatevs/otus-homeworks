public class UnspawnMechanic
{
    private readonly Entity _entity;
    private readonly AtomicVariable<bool> _isDeactivated;
    private readonly IAtomicAction<Entity> _OnUnspawn;
    private readonly ZombieInitParams _initParams;
    private readonly IAtomicVariable<bool> _isAttacking;
    private readonly IAtomicVariable<bool> _canMove;
    private readonly IAtomicVariable<int> _hp;
    private readonly IAtomicVariable<bool> _isDead;
    
    public UnspawnMechanic(Entity entity, 
        AtomicVariable<bool> isDeactivated,
        IAtomicAction<Entity> OnUnspawn,
        ZombieInitParams initParams,
        IAtomicVariable<bool> isAttacking,
        IAtomicVariable<bool> canMove,
        IAtomicVariable<int> hp,
        IAtomicVariable<bool> isDead)
    {
        _entity = entity;
        _isDeactivated = isDeactivated;
        _OnUnspawn = OnUnspawn;
        _initParams = initParams;
        _isAttacking = isAttacking;
        _canMove = canMove;
        _hp = hp;
        _isDead = isDead;
    }

    public void OnEnable()
    {
        _isDeactivated.Subscribe(OnDeactivate);
    }

    public void OnDisable()
    {
        _isDeactivated.Unsubscribe(OnDeactivate);
    }

    private void OnDeactivate(bool isDeactivated)
    {
        if (!isDeactivated)
        {
            return;
        }
        else
        {
            _OnUnspawn.Invoke(_entity);

            _isAttacking.Value = _initParams.isAttacking;
            _canMove.Value = _initParams.canMove;
            _hp.Value = _initParams.hp;
            _isDead.Value = _initParams.isDead;
        }
    }
}