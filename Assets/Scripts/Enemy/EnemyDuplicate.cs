using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDuplicate : EnemyController
{

    [SerializeField] private GameObject _miniEnemies;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (m_hp <= 0)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(_miniEnemies, transform.position+transform.up, Quaternion.identity);
            }
        }
    }
}
