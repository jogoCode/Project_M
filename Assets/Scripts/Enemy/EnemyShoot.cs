using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyController
{
    [Header("EnemyShoot")]

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _pos;

    [SerializeField] private float _nextShootTime;
    [SerializeField] private float _delay;

    public static Action OnShoot;

    protected override void Start()
    {
        base.Start();
        //OnShoot += Shoot;
    }

    protected override void MoveTowardsPlayer()
    {

        if (_isShooting == true)
        {
            //Debug.Log("shoot");                 
            //EnemyShoot.OnShoot();
            Shoot();
        }
        base.MoveTowardsPlayer();
    }

    void Shoot()
    {

            if (Time.time >= _nextShootTime)
            {
                if (_pos != null)
                {
                    Instantiate(_bullet, _pos.position, _pos.rotation);
                }
                _nextShootTime = Time.time + _delay;
            }
    }
    
}
