using System.Collections.Generic;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class GameManagerInstaller :
        IInitializable
    {
        private GameManagerData _gameManagerData;

        private IEnumerable<IGameListener> _injectedListeners;

        public GameManagerInstaller(GameManagerData gameManagerData, IEnumerable<IGameListener> listeners)
        {
            _gameManagerData = gameManagerData;
            _injectedListeners = listeners;
        }

        public void Initialize()
        {
            _gameManagerData.AddListeners(_injectedListeners);
        }
    }
}