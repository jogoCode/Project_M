using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Weapon m_weaponData;
    [SerializeField] Items m_itemData;
    [SerializeField] GameObject m_visualParent;

    bool m_canEquip = true;
    private void Start()
    {
        EquipWeapon(m_weaponData);
        Pickable.OnPickedUp += EquipWeapon;
    }


    private void Update()
    {

    }

    public void EquipWeapon(Weapon newData)
    {
        if (!m_canEquip) return;
        m_weaponData = newData;
        if (!m_weaponData) return;
        ChangeVisual(m_weaponData.Prefabs);
    }

    public void EquipItem(Items newData)
    {
        m_weaponData = null;
        m_itemData = newData;
        ChangeVisual(m_itemData.Prefabs);
    }

    void ChangeVisual(GameObject newVisual)
    {
        int child = m_visualParent.transform.childCount;
        for (int i = 0; i < child; i++)
        {
            Destroy(m_visualParent.transform.GetChild(i).gameObject);
        }

        var handObject = Instantiate(newVisual,m_visualParent.transform.position, Quaternion.Euler(-90,0,0), m_visualParent.transform);
        handObject.transform.up = m_visualParent.transform.up;
        handObject.transform.forward = m_visualParent.transform.forward;
    }

    public void RemoveItem()
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
