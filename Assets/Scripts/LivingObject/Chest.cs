using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : LivingObject
{
    [Header("Chest")]

    [SerializeField] private GameObject[] _items;
    [SerializeField] private bool _isHurt;
    [SerializeField] private ParticleSystem _fx;

    [SerializeField] private bool _canBeHurt;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        var player = other.GetComponentInParent<PlayerController>();

        if(player != null)
        {
            if (m_hp <= 0)
            {
                RandomWeapon();
                Die(player);
                Destroy(gameObject);

                if (_isHurt)
                {
                    player.SetHp(-10);
                }
            }
    }

    void RandomWeapon()
        {
            //* POURCENTAGE DROP
            int die = Random.Range(0, 100);
            Debug.Log(die);


                //DAMAGES (10%)
                if (die <= 10)
                {
                    if (_canBeHurt)
                    {
                        _isHurt = true;
                        Instantiate(_fx, transform.position, transform.rotation);
                    }
                    else 
                    {
                        Instantiate(_items[1], transform.position, transform.rotation);
                    }
                }
                //APPLE (20%)
                if (die >= 10 && die <= 30)
                {
                    Instantiate(_items[2], transform.position, transform.rotation);
                }
                //EPEE (30%)
                if (die >= 30 && die <= 60)
                {
                    Instantiate(_items[1], transform.position, transform.rotation);
                }
                //STICK (40%)
                if (die >= 60)
                {
                    Instantiate(_items[0], transform.position, transform.rotation);
                }         

            /*/ RANDOM DROP
            int randomWeapon = Random.Range(0, _items.Length);

            Instantiate(_items[randomWeapon], transform.position, transform.rotation);
            //*/

        }
    }
}
