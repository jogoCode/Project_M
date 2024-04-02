using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : LivingObject
{

    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private bool _isHurt;
    [SerializeField] private ParticleSystem _fx;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        var player = other.GetComponentInParent<PlayerController>();

        if (m_hp <= 0)
        {
            RandomWeapon();
            Die(player);
            Destroy(gameObject);

            if(_isHurt) 
            {       
            player.SetHp(-10);
            }
    }

    void RandomWeapon()
        {
            //* POURCENTAGE DROP
            int die = Random.Range(0, 100);
            Debug.Log(die);

            if (die <= 20) 
            {
                _isHurt = true;
                Instantiate(_fx, transform.position, transform.rotation);
            }
            if(die >= 20 && die <= 70) 
            {
                Instantiate(_weapons[0], transform.position, transform.rotation);
            }
            if (die >= 70)
            {
                Instantiate(_weapons[1], transform.position, transform.rotation);
            }

            /*/ RANDOM DROP
            int randomWeapon = Random.Range(0, _weapons.Length);

            Instantiate(_weapons[randomWeapon], transform.position, transform.rotation);
            //*/

        }
    }
}
