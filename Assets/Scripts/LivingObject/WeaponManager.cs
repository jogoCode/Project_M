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

    [SerializeField] bool m_hideWeapon;


    private void Start()
    {
        m_firstEquip = true;
        if(!m_itemData)
        {
            EquipWeapon(m_weaponData);
        }
        else if(!m_weaponData)
        {
            EquipItem(m_itemData);
        }
       
        Pickable.OnPickedUp += EquipWeapon;
        if (m_hideWeapon)
        {
            HideWeapon();
        }
    }

    public void EquipWeapon(Weapon newData) // EQUIPE UNE ARME
    {
        DropObject(false);
        m_weaponData = newData;
        if (!m_weaponData) return;
        ChangeVisual(m_weaponData.Prefabs);
        
    }

    public void EquipItem(Items newData) // EQUIPE UN OBJET
    {
        DropObject(false);
        m_weaponData = null;
        m_itemData = newData;
        if (!m_itemData) return;
        ChangeVisual(m_itemData.Prefabs);
    }

    void ChangeVisual(GameObject newVisual) // CHANGE LE VISUEL DE L'OBJET EN MAIN
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

        Vector3 dropPos = new Vector3(CastARay().x, 1+CastARay().y, CastARay().z);
        if (m_weaponData!= null)
        {
            var lastItemPref = Instantiate(m_pickable);
            lastItemPref.GetComponent<Seeitem>().SetWeapon(m_weaponData);
            m_weaponData = null;
            if (isDrop)
            {
                
                ChangeVisual(null);
            }
        }
        if (m_itemData != null)
        {
            var lastItemPref = Instantiate(m_pickable, dropPos, Quaternion.identity);
            lastItemPref.GetComponent<Seeitem>().SetItem(m_itemData);
            m_itemData = null;
            if (isDrop)
            {
                
                ChangeVisual(null);
            }
        }

    } // LACHE L'OBJET EN MAIN AU SOL

    public Vector3 CastARay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; 
        if(Physics.Raycast(ray,out hit,2))
        {
            return hit.point;
        }
        return transform.position+transform.forward;
    }

    public void HideWeapon() // CACHE L'ARME EN MAIN
    {
        m_visualParent.SetActive(false);
    }

    public void UseItem() //UTILISE L'ITEM EN MAIN
    {
        m_itemData = null;
        Destroy(m_visualParent.transform.GetChild(0).gameObject);
    }
    //-----------------------------------GET---------------------
    public Weapon GetWeaponData() 
    {
        return m_weaponData;
    }
    public GameObject GetVisualData()
    {
        return m_visualParent;
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
