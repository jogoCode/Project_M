using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Weapon m_weaponData;
    [SerializeField] GameObject m_visualParent;

    private void Start()
    {
        EquipWeapon(m_weaponData);
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            ChangeVisual(m_weaponData.Prefabs);
        }
    }

    void EquipWeapon(Weapon newData)
    {
        m_weaponData = newData;
        ChangeVisual(m_weaponData.Prefabs);
    }

    void ChangeVisual(GameObject newVisual)
    {
        GameObject child = m_visualParent.transform.GetChild(0).gameObject;
        Instantiate(newVisual,m_visualParent.transform.position, Quaternion.Euler(-90,0,0), m_visualParent.transform);
        Destroy(child);
    }


    //-----------------------------------GET---------------------

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
