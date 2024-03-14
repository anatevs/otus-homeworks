public class IsAttackingComponent : IIsAttackingComponent
{
    private readonly IAtomicVariable<bool> _isAttacking;

    public IsAttackingComponent(IAtomicVariable<bool> isAttacking)
    {
        _isAttacking = isAttacking;
    }

    public void SetIsAttacking(bool isAttacking)
    {
        _isAttacking.Value = isAttacking;
    }
}