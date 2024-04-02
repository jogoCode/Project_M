using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : LivingObject
{

    [SerializeField] private GameObject[] _weapons;

    private void Update()
    {
        //DESTROY AND INSTANTIATE RANDOM WEAPON
        if (m_hp <= 0)
        {
            RandomWeapon();
            Destroy(gameObject);
        }
    }
    void RandomWeapon()
    {

    int randomWeapon = Random.Range(0, _weapons.Length);

    Instantiate(_weapons[randomWeapon], transform.position, transform.rotation);

    }
}
