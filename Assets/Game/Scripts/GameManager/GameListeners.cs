public interface IGameListener
{

}

public interface IFinishGameListener : IGameListener
{
    public void OnFinishGame();
}