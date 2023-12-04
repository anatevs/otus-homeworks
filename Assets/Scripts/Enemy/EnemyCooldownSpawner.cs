using ShootEmUp;
using System.Collections;
using UnityEngine;

public class EnemyCooldownSpawner : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private float _cooldownTime = 1f;

    //TODO:
    private bool _isGamePlaying = true;

    private IEnumerator Start()
    {
        while (_isGamePlaying)
        {
            yield return new WaitForSeconds(_cooldownTime);
            _enemyManager.SpawnEnemy();
        }
    }
}
