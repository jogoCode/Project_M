using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObject : MonoBehaviour , ILivingObject
{

    [Header("Enemy")]
    //Life
    [SerializeField] protected int m_hp;
    [SerializeField] protected int m_maxhp;
    [SerializeField] protected int m_armor;

    public int GetArmor()
    {
        return m_armor;

    }

    public void Attack()
    {
       
    }

    public void Die()
    {
       if (m_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        
    }

    public void SetHp(int hp)
    {
        m_hp += hp; 
    }


  
}
