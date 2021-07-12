using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;

    public bool HasEnemies()
    {
        if (_enemies == null)
            return true;

        foreach (var enemy in _enemies)
            if (enemy.enabled)
                return false;

        return true;
    }
}
