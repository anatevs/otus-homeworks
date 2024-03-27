public interface IGameListener
{

}

public interface IFinishGameListener : IGameListener
{
    public void OnFinishGame();
}

public interface IEndGameListener : IGameListener
{
    public void OnEndGame();
}