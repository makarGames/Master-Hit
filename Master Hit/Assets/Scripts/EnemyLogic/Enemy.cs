using PlayerLogic;
using UnityEngine;

namespace EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _bones;

        private void Awake()
        {
            SetKinematic(true);
        }

        public void Death()
        {
            _animator.enabled = false;
            GetComponent<Collider>().enabled = false;
            SetKinematic(false);

            this.enabled = false;
        }

        private void FixedUpdate()
        {
            transform.LookAt(Player.S.transform);
        }

        private void SetKinematic(bool isKinematic)
        {
            foreach (var bone in _bones)
                bone.isKinematic = isKinematic;
        }
    }
}
