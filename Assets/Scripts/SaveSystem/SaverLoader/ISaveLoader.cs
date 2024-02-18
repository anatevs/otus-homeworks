public interface ISaveLoader
{
    public void Save(IGameRepository gameRepository);

    public void Load(IGameRepository gameRepository);
}