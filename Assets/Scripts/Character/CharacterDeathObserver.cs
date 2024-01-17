using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterDeathObserver :
        IStartGame,
        IFinishGame
    {
        private readonly HitPointsComponent _hpComponent;

        private readonly GameManager _gameManager;

        public CharacterDeathObserver(CharacterComponents characterComponents, GameManager gameManager)
        {
            _hpComponent = characterComponents.HitPointsComponent;
            _gameManager = gameManager;
        }

        public void OnStart()
        {
            _hpComponent.OnHPZero += OnCharacterDeath;
        }

        public void OnFinish()
        {
            if (_hpComponent != null)
            {
                _hpComponent.OnHPZero -= OnCharacterDeath;
            }
        }

        private void OnCharacterDeath(HitPointsComponent _) => _gameManager.FinishGame();
    }
}