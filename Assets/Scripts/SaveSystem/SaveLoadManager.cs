using VContainer.Unity;

public class SaveLoadManager :
    IPostInitializable,
    IAppQuitListener
{
    private ISaveLoader[] _saverLoaders;

    private SaveLoadUnits _saveLoadUnits;

    private SaveLoadResources _saveLoadResources;

    private GameRepository _gameRepository;

    public SaveLoadManager(SaveLoadUnits saveLoadUnits, SaveLoadResources saveLoadResources, GameRepository gameRepository)
    {
        _saveLoadUnits = saveLoadUnits;
        _saveLoadResources = saveLoadResources;
        _saverLoaders = new ISaveLoader[] {_saveLoadUnits, _saveLoadResources};
        _gameRepository = gameRepository;
    }

    public void PostInitialize()
    {
        _gameRepository.LoadState();

        foreach (var loader in _saverLoaders)
        {
            loader.Load(_gameRepository);
        }
    }

    public void OnAppQuit()
    {
        foreach (var loader in _saverLoaders)
        {
            loader.Save(_gameRepository);
        }
        _gameRepository.SaveState();
    }
}