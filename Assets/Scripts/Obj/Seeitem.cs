using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Seeitem : Pickable
{
    [SerializeField] Items _item;
    //[SerializeField] Seeitem _level;
    [SerializeField] Weapon _weapon;

    private void Start()
    {
        if (_item != null)
        {
            Instantiate(_item.Prefabs, transform.position, Quaternion.identity, this.transform);
        }else if (_weapon != null)
        {
            Instantiate(_weapon.Prefabs, transform.position, Quaternion.identity, this.transform);
        }
        
    }
}
