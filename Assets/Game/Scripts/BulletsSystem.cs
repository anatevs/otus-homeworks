using UnityEngine;
using VContainer;

public class BulletsSystem : MonoBehaviour
{
    private PoolManager<Bullet> _bulletPool;

    [Inject]
    public void Construct(PoolManager<Bullet> pool)
    {
        _bulletPool = pool;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Bullet bullet = _bulletPool.Spawn();
    }
}