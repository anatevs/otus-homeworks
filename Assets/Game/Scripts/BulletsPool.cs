using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : MonoBehaviour 
{
    [SerializeField]
    private Transform _worldTransform;

    [SerializeField]
    private Transform _poolTransform;

    [SerializeField]
    private Bullet _prefab;

    private int _initCount = 10;

    private PoolManager<Bullet> _poolManager;

    private void Awake()
    {
        
    }
}
