using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffable : MonoBehaviour
{
    [Header("Buffable")]
    Buffs m_selectedBuff;
    enum Buffs
    {
        ATK,
        HP,
        JUMP,
        SPEED,
        XPBOOST,
        ARMOR,
        DASH
    }
    [SerializeField] protected LivingObject m_buffTarget;
    void Start()
    {
        m_buffTarget = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {

        }
    }

    public void AddAtk(int dmg)
    {
        m_buffTarget.SetDmgBuff(dmg);
    }

    public void AddHp(int hp)
    {
        m_buffTarget.SetMaxHp(hp);
    }
}
