public interface IAtomicAction
{
    public void Invoke();
}

public interface IAtomicAction<T>
{
    public void Invoke(T action);
}