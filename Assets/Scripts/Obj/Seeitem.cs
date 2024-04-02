using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

public class Seeitem : Pickable
{
    [SerializeField] Items _item;
    //[SerializeField] Seeitem _level;
    [SerializeField] Weapon _weapon;

    bool _isWeapon;
    private void Start()
    {
        if (_item != null)
        {
            Instantiate(_item.Prefabs, transform.position, Quaternion.identity, this.transform);
            _isWeapon = false;
        }else if (_weapon != null)
        {
            Instantiate(_weapon.Prefabs, transform.position, Quaternion.identity, this.transform);
            _isWeapon = true;
        }
        else
        {
            return;
        }
        
    }

    override public void Pick(LivingObject owner)
    {
        //OnPickedUp(_weapon);
        WeaponManager weaponManager = owner.GetComponent<WeaponManager>();
        if (_isWeapon)
        {
            weaponManager.EquipWeapon(_weapon);
        }
        else
        {
            weaponManager.EquipItem(_item);
        }
        Destroy(gameObject);
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<LivingObject>())
    //    {
    //        Debug.Log("j'ai attrapé quelque chose");
    //        //WeaponManager weaponManager = other.GetComponent<WeaponManager>();
    //        //if (_isWeapon)
    //        //{
    //        //    weaponManager.EquipWeapon(_weapon);
    //        //}
    //        //else
    //        //{
    //        //    weaponManager.EquipItem(_item);
    //        //}
            
    //        //Destroy(gameObject);
    //    }
    //}


    public void SetWeapon(Weapon newWeapon)
    {
        _weapon = newWeapon ;
    }

    public void SetItem(Items newItem)
    {
        _item = newItem;
    }


}
