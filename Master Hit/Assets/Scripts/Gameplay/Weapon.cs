using Pool;
using UnityEngine;

namespace Gameplay
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private BulletPool _pool;
        [SerializeField] private ParticleSystem _shot;

        public void Shoot()
        {
            RaycastHit hit;
            var pos = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(new Vector3(pos.x, pos.y, 0));

            if (Physics.Raycast(ray, out hit))
            {
                _pool.CreateBullet(hit.point);

                if (_shot != null)
                    _shot.Play();
            }
        }
    }
}
