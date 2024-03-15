public class CanMoveSetComponent : ICanMoveSetComponent
{
    private IAtomicVariable<bool> _canMove;

    public CanMoveSetComponent(IAtomicVariable<bool> canMove)
    {
        _canMove = canMove;
    }

    public void SetCanMove(bool canMove)
    {
        _canMove.Value = canMove;
    }
}