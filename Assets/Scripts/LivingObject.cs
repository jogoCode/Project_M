using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingObject : MonoBehaviour , ILivingObject
{
    //Life
    [SerializeField] protected int m_hp;
    [SerializeField] protected int m_maxhp;
    [SerializeField] protected int m_armor;



    public void Attack()
    {
       
    }

    public void Die()
    {
       
    }

    public void Hit()
    {
       
    }


    public void Movement()
    {
        
    }

    public void SetHp(int hp)
    {
        m_hp += hp; 
    }



  
}
