using System.Collections.Generic;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _healthPoints;

        private int _healthAmount;
        public int healthAmount
        {
            get => _healthAmount;
            set
            {
                _healthAmount = value;
                _healthPoints[_healthAmount].SetActive(false);
                if (healthAmount == 0)
                    GetComponent<Enemy>().Death();
            }
        }

        private void Start()
        {
            _healthAmount = _healthPoints.Count;
        }
    }
}
