using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent hpComponent;
        [SerializeField] private GameManager gameManager;

        private void OnEnable()
        {
            this.hpComponent.HPempty += this.OnCharacterDeath;
        }

        private void OnDisable()
        {
            if (hpComponent != null)
            {
                this.hpComponent.HPempty -= this.OnCharacterDeath;
            }
        }
        private void OnCharacterDeath(GameObject _) => this.gameManager.FinishGame();
    }
}