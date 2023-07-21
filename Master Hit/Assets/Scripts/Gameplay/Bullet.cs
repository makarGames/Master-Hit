using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField, Range(0f, 50f)] private float _speed = 10f;
        [SerializeField, Range(0f, 30f)] private float _lifeTime = 3f;

        [SerializeField] private TrailRenderer _trail;

        private Vector3 _direction;
        private Transform _transform;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void OnEnable()
        {
            _direction = _transform.position;
            StartCoroutine(LifeRoutine());
        }

        private void OnDisable()
        {
            if (_trail != null)
                _trail.Clear();
            StopCoroutine(LifeRoutine());
        }

        private IEnumerator LifeRoutine()
        {
            yield return new WaitForSeconds(_lifeTime);
            Deactive();
        }

        private void FixedUpdate()
        {
            float step = _speed * Time.deltaTime;
            _transform.position = Vector3.MoveTowards(_transform.position, _direction, step);
        }

        public void Project(Vector3 direction)
        {
            _direction = direction;
        }

        private void Deactive()
        {
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision other)
        {
            Deactive();
        }
        private void OnTriggerEnter(Collider other)
        {
            Deactive();
        }
    }
}
