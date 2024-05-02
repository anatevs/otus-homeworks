using VContainer;

public interface ISaveLoader
{
    public void Save(IGameRepository gameRepository, IObjectResolver context);

    public void Load(IGameRepository gameRepository, IObjectResolver context);
}