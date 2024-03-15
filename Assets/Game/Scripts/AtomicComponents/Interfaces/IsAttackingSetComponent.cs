public class IsAttackingSetComponent : IIsAttackingSetComponent
{
    private IAtomicVariable<bool> _isAttacking;

    public IsAttackingSetComponent(IAtomicVariable<bool> isAttacking)
    {
        _isAttacking = isAttacking;
    }

    public void SetIsAttacking(bool isAttacking)
    {
        _isAttacking.Value = isAttacking;
    }
}