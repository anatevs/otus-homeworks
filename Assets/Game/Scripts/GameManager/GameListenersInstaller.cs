using System.Collections.Generic;
using VContainer.Unity;

public class GameListenersInstaller : IPostInitializable
{
    private readonly GameListenersContainer _listenersContainer;
    private readonly IEnumerable<IGameListener> _injectedListeners;

    public GameListenersInstaller(
        GameListenersContainer listenersContainer, 
        IEnumerable<IGameListener> injectedListeners)
    {
        _listenersContainer = listenersContainer;
        _injectedListeners = injectedListeners;
    }

    public void PostInitialize()
    {
        _listenersContainer.AddListeners(_injectedListeners);
    }
}