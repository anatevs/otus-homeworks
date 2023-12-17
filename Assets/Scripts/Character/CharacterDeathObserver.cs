using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver : MonoBehaviour,
        IStartGame,
        IFinishGame
    {
        [SerializeField] private HitPointsComponent _hpComponent;
        [SerializeField] private GameManager _gameManager;

        public void OnStart()
        {
            _hpComponent.OnHPempty += OnCharacterDeath;
        }

        public void OnFinish()
        {
            if (_hpComponent != null)
            {
                _hpComponent.OnHPempty -= OnCharacterDeath;
            }
        }

        private void OnCharacterDeath(GameObject _) => _gameManager.FinishGame();

        
    }
}