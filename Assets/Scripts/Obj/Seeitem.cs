using System.Collections;
using System.Collections.Generic;
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
            GameObject visual = Instantiate(_weapon.Prefabs, transform.position, Quaternion.identity, this.transform);
            GetComponent<BoxCollider>().size = transform.localScale;
            _isWeapon = true;
        }
        else
        {
            return;
        }
      
    }

    override public void Pick(LivingObject owner)
    {
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

    public void SetWeapon(Weapon newWeapon)
    {
        _weapon = newWeapon ;
    }

    public void SetItem(Items newItem)
    {
        _item = newItem;
    }


}
