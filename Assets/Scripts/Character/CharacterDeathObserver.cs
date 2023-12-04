using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hpComponent;
        [SerializeField] private GameManager _gameManager;

        private void OnEnable()
        {
            this._hpComponent.HPempty += this.OnCharacterDeath;
        }

        private void OnDisable()
        {
            if (_hpComponent != null)
            {
                this._hpComponent.HPempty -= this.OnCharacterDeath;
            }
        }
        private void OnCharacterDeath(GameObject _) => this._gameManager.FinishGame();
    }
}