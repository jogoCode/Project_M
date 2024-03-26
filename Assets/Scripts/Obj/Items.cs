using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


[CreateAssetMenu(fileName = "New Item", menuName = "item")]
public class Items : ScriptableObject
{
    /* ajouter la possibiliter de drop un item
     * 
     */

    public string _name;
    [SerializeField] int _hpgain;
    [SerializeField] int _damage;
    [SerializeField] GameObject _prefabs;

    public string Name
    {        
        get { return _name; }
    }
    public int Hpgain
    {
        get { return _hpgain; }
    }

    public int Damage
    {
        get { return _damage; }
    }
    public GameObject Prefabs
    {
        get { return _prefabs; }
    }
}



