using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pomme : Pickable
{
    public string _name;
    [SerializeField] int _hpgain;
    [SerializeField] int _damage;
    [SerializeField] GameObject _prefabs;


    private void Start()
    {
        Instantiate(_prefabs, transform.position, Quaternion.identity);
    }

    public override void Pick(LivingObject owner)
    {
       
    }
}
