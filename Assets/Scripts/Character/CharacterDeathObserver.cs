using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver :
        IStartGame,
        IFinishGame
    {
        private HitPointsComponent _hpComponent;
        private GameManager _gameManager;

        public CharacterDeathObserver(CharacterComponents characterComponents, GameManager gameManager)
        {
            _hpComponent = characterComponents.GetComponent<HitPointsComponent>();
            _gameManager = gameManager;
        }

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