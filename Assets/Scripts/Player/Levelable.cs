using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelable : MonoBehaviour, ILevelable
{
    int m_lvl = 1;
    float m_exp = 0;
    float m_maxExp;
    float m_expToAdd;


    private void Start()
    {
        SetMaxExp();
    }

    public void AddExp(float amount)
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

    public bool CanLvlUp(float amount)
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

    public void SetMaxExp()
    {
        m_maxExp = GetExpForLvl(m_lvl);
    }

    public void AddLvl()
    {
        Debug.Log("LEVEL UP");
        m_lvl++;
        SetMaxExp();
    }

    //----------------------------------- Get

    public int GetLevel()
    {
        return m_lvl;
    }

    public float GetExpForLvl(int lvl)
    {
        return (5 * (lvl * lvl * lvl)) / 4;
        //return (6 / 5) * (lvl * lvl * lvl) - 15 * (lvl * lvl) + 100 * lvl - 140;
        //return lvl * lvl * lvl;
        //return 4 * (lvl * lvl * lvl) / 5; rapide
    }

    public float GetExp()
    {
        return m_exp;
    }

    public float GetMaxExp()
    {
        return m_maxExp;
    }


}
