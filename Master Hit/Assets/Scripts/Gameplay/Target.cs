using System.Collections;
using System.Collections.Generic;
using EnemyLogic;
using Gameplay;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            if (this.GetComponent<EnemyHealth>())
                this.GetComponent<EnemyHealth>().healthAmount--;
            else
                gameObject.SetActive(false);
        }
    }
}
