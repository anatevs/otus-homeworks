using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver : MonoBehaviour,
        GameListeners.IStartGame,
        GameListeners.IFinishGame
    {
        [SerializeField] private HitPointsComponent _hpComponent;
        [SerializeField] private GameManager _gameManager;

        public void OnStart()
        {
            this._hpComponent.OnHPempty += this.OnCharacterDeath;
        }

        public void OnFinish()
        {
            if (_hpComponent != null)
            {
                this._hpComponent.OnHPempty -= this.OnCharacterDeath;
            }
        }

        private void OnCharacterDeath(GameObject _) => this._gameManager.FinishGame();

        
    }
}