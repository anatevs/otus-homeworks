public interface IAtomicAction
{
    public void Invoke();
}

public interface IAtomicAction<in T>
{
    public void Invoke(T action);
}