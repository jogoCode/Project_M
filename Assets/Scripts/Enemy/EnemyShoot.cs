using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyController
{
    [Header("EnemyShoot")]

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _pos;
    [SerializeField] private Transform _bullets;

    [SerializeField] private float _nextShootTime;
    [SerializeField] private float _delay;

    [SerializeField] private Transform _boxDetect;
    [SerializeField] private Vector3 _radBox;

    public static Action OnShoot;

    protected override void Start()
    {
        base.Start();
        //OnShoot += Shoot;
    }
    protected override void Update()
    {
        base.Update();
        //SHOOT
        if (_boxDetect != null)
        {
            Distance();
        }
        else
        {
            return;
        }

    }
    //SHOOT AT LONG DISTANCE
    void Distance()
    {
        Collider[] player = Physics.OverlapBox(_boxDetect.position, _radBox / 2);
        foreach (Collider detection in player)
        {
            if (detection.GetComponent<PlayerController>() != null)
            {
                _isShooting = true;

            }
        }
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
                    GameObject bullet = Instantiate(_bullet, _pos.position, _pos.rotation);
                    bullet.GetComponent<Bullet>().SetDammage(m_weapon.GetWeaponData().Damage);
                }
                _nextShootTime = Time.time + _delay;
            }
    }

    //DETECTION RADIUS
    private void OnDrawGizmos()
    {
        if (_boxDetect == null)
        {
            return;
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_boxDetect.position, _radBox);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_sphere.position, _radius);
    }


}
