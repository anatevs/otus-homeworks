using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using UnityEngine;

public sealed class BasesHelper : MonoBehaviour
{
    [SerializeField]
    private BaseProvider _base;

    [ShowInInspector]
    private int Health => _health;
    
    private int _health;

    [ShowInInspector, ReadOnly]
    private TeamType _baseTeam;

    [SerializeField]
    private Vector3 _spawnPoint;

    private Collider _groundCollider;

    private Transform _spawnTransform;

    private Camera _camera;
    private Ray _ray;

    private void Start()
    {
        _health = _base.Entity.GetComponent<Health>().value;

        _baseTeam = _base.Entity.GetComponent<Team>().value;

        _groundCollider = GetComponent<Collider>();

        _camera = Camera.main;

        GameObject go = new GameObject();
        go.transform.SetParent(transform);
        _spawnTransform = go.transform;
    }

    private void Update()
    {
        _health = _base.Entity.GetComponent<Health>().value;

        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (_groundCollider.Raycast(_ray, out RaycastHit hitData, 1000))
            {
                _spawnPoint = hitData.point;
                _spawnPoint.y = 0;
                _spawnTransform.position = _spawnPoint;
            }
        }
    }

    [Button]
    private void CreateArcher()
    {
        CreateMob(ObjectsTypeNames.Archer);
    }

    [Button]
    private void CreateSwordman()
    {
        CreateMob(ObjectsTypeNames.Swordman);
    }

    private void CreateMob(ObjectsTypeNames mobType)
    {
        _base.Entity.AddComponent<SpawnRequest>() = new SpawnRequest()
        {
            type = mobType,
            team = _baseTeam,
            transform = _spawnTransform
        };
    }
}