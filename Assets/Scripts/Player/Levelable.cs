using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class Levelable : MonoBehaviour, ILevelable
{
    [Header("Levelable")]
    int m_lvl = 1;
    float m_exp = 0;
    float m_maxExp;


    public static Action isLevelUp;

    private void Start()
    {
        SetMaxExp();
    }

    public void AddExp(float amount)  //Ajoute de l'exp
    {
        if (CanLvlUp(amount))
        {
            m_exp = 5 - (m_maxExp - m_exp);
            AddLvl();
        }
        else
        {
            m_exp += amount;
        }
    }

    public bool CanLvlUp(float amount) // verifie si on peut level up 
    {
        if (m_exp + amount > m_maxExp)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetMaxExp() // Modifie la valeur d'exp maximale
    {
        m_maxExp = GetExpForLvl(m_lvl);
    }

    public void AddLvl() // Level Up
    {
        //Debug.Log("LEVEL UP");
        isLevelUp();
        m_lvl++;
        SetMaxExp();
    }

    //----------------------------------- GET-----------//

    public int GetLevel()
    {
        return m_lvl;
    } // Recupère le Lvl

    public float GetExpForLvl(int lvl) //Recupère la valeur d'exp maximal pour un lvl
    {
        return (5 * (lvl * lvl * lvl)) / 2;
        //return (6 / 5) * (lvl * lvl * lvl) - 15 * (lvl * lvl) + 100 * lvl - 140;
        //return lvl * lvl * lvl;
        //return 4 * (lvl * lvl * lvl) / 5; rapide
    }

    public float GetExp() // Recupère la valeur d'exp
    {
        return m_exp;
    } 

    public float GetMaxExp() // Recupère la valeur d'exp maximale
    {
        return m_maxExp;
    }


}
