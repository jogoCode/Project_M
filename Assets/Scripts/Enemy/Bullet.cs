using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    [SerializeField] private float _destroyedTime;
    
    int _dammage;
    void Start()
    {
        Destroy(gameObject, _destroyedTime);
    }

    void Update()
    {
        transform.Translate(transform.forward * _bulletSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerController>() != null) 
        {
            other.gameObject.GetComponent<PlayerController>().SetHp(-_dammage);
        }
    }

    public void SetDammage(int dmgValue)
    {
        _dammage = dmgValue;
    }

}
