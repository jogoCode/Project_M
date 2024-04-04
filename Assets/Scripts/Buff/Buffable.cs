using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffable : MonoBehaviour
{
    [Header("Buffable")]

    [SerializeField] protected LivingObject m_buffParent;
    void Start()
    {
        m_buffParent = GetComponent<LivingObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddHp(40);
        }
    }

    public void AddAtk(int dmg)
    {
        m_buffParent.SetDmgBuff(dmg);
    }

    public void AddHp(int hp)
    {
        m_buffParent.SetMaxHp(hp);
    }
}
