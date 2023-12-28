using System.Collections.Generic;
using System.Linq;
using VContainer.Unity;

namespace ShootEmUp
{
    public class GameManagerInstaller : 
        IInitializable
    {
        private GameManager _gameManager;
        private IGameListener[] _injectedListeners;

        public GameManagerInstaller(GameManager gameManager, IEnumerable<IGameListener> listeners)
        {
            _gameManager = gameManager;
            _injectedListeners = listeners.ToArray();
        }

        public void Initialize()
        {
            _gameManager.AddListeners(_injectedListeners);
        }
    }
}