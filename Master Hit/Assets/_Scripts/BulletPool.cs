using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int _poolCount = 10;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _burrelTransform;

    private PoolMono<Bullet> _pool;

    private void Start()
    {
        _pool = new PoolMono<Bullet>(_bulletPrefab, _poolCount, transform);
        _pool.autoExpand = _autoExpand;
    }

    public void CreateBullet(Vector3 direction)
    {
        var bullet = _pool.GetFreeElement();
        RestPosition(bullet);
        bullet.Project(direction);
    }

    private void RestPosition(Bullet bullet)
    {
        bullet.transform.position = _burrelTransform.position;
    }
}
