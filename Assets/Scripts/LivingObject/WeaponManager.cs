using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Weapon m_weaponData;
    [SerializeField] Items m_itemData;
    [SerializeField] GameObject m_visualParent;

    [SerializeField] GameObject m_pickable;

    [SerializeField] bool m_firstEquip = true;

    private void Start()
    {
        m_firstEquip = true;
        EquipWeapon(m_weaponData);
        Pickable.OnPickedUp += EquipWeapon;
    }


    private void Update()
    {

    }

    public void EquipWeapon(Weapon newData)
    {
        DropObject(false);
        m_weaponData = newData;
        if (!m_weaponData) return;
        ChangeVisual(m_weaponData.Prefabs);
        
    }

    public void EquipItem(Items newData)
    {
        DropObject(false);
        m_weaponData = null;
        m_itemData = newData;
        if (!m_itemData) return;
        ChangeVisual(m_itemData.Prefabs);
    }

    void ChangeVisual(GameObject newVisual)
    {
        m_firstEquip = false;
        int child = m_visualParent.transform.childCount;
        for (int i = 0; i < child; i++)
        {
            Destroy(m_visualParent.transform.GetChild(i).gameObject);
        }
        if (!newVisual) return;
        var handObject = Instantiate(newVisual,m_visualParent.transform.position, Quaternion.Euler(-90,0,0), m_visualParent.transform);
        handObject.transform.up = m_visualParent.transform.up;
        handObject.transform.forward = m_visualParent.transform.forward;
    }

    public void DropObject(bool isDrop)
    {
        if (m_firstEquip) return;

        if (m_weaponData!= null)
        {
            var lastItemPref = Instantiate(m_pickable, transform.position+transform.forward*2, Quaternion.identity);
            lastItemPref.GetComponent<Seeitem>().SetWeapon(m_weaponData);
            m_weaponData = null;
            if (isDrop)
            {
                
                ChangeVisual(null);
            }
        }
        if (m_itemData != null)
        {
            var lastItemPref = Instantiate(m_pickable, transform.position + transform.forward * 2, Quaternion.identity);
            lastItemPref.GetComponent<Seeitem>().SetItem(m_itemData);
            m_itemData = null;
            if (isDrop)
            {
                
                ChangeVisual(null);
            }
        }

    }

    public void UseItem()
    {
        m_itemData = null;
        Destroy(m_visualParent.transform.GetChild(0).gameObject);
    }
    //-----------------------------------GET---------------------
    public Weapon GetWeaponData()
    {
        return m_weaponData;
    }

    public Items GetItemData()
    {
        return m_itemData;
    }

    public float GetDamage()
    {
        return m_weaponData.Damage;
    }

    public float GetAtkSpeed()
    {
        return m_weaponData.AtkSpeed;
    }

    public float GetRange()
    {
        return m_weaponData.Range;
    }

}
