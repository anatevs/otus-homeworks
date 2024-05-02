using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

public class SaveLoadManager :
    IPostInitializable,
    IAppQuitListener
{
    private readonly IEnumerable<ISaveLoader> _saveLoaders;

    private readonly GameRepository _gameRepository;

    private readonly IObjectResolver _context;

    public SaveLoadManager(IEnumerable<ISaveLoader> saveLoaders, GameRepository gameRepository, IObjectResolver context)
    {
        _saveLoaders = saveLoaders;
        _gameRepository = gameRepository;
        _context = context;
    }

    public void PostInitialize()
    {
        _gameRepository.LoadState();

        foreach (var loader in _saveLoaders)
        {
            loader.Load(_gameRepository, _context);
        }
    }

    public void OnAppQuit()
    {
        foreach (var loader in _saveLoaders)
        {
            loader.Save(_gameRepository, _context);
        }
        _gameRepository.SaveState();
    }
}