using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseABuff : MonoBehaviour
{

    PlayerController m_playerTarget;
    [SerializeField] GameObject m_buffButton;
    [SerializeField] Vector3 m_displayPos;
    [SerializeField] float m_displayOffset;
    [SerializeField] GameObject m_buffsParent;

    Buffs m_buffType;
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

    public void Start()
    {
        if (!m_buffButton)
        {
            Debug.LogError("Il manque le préfabs du buffButton "+this.gameObject);
        }
        UpdateBuff();
    }

    public void UpdateBuff()
    {
        for (int i = 0; i< 3; i++)
        {
            Instantiate(m_buffButton,new Vector3(m_displayPos.x+i* m_displayOffset, m_displayPos.y, m_displayPos.z), Quaternion.identity,m_buffsParent.transform);
        }
    }


    public void RandBuff()
    {
        var rand = Random.Range(0,5);
        switch (rand){
            case 0: 
                m_buffType = Buffs.ATK; 
            break;
            case 1:
                m_buffType = Buffs.HP;
            break;
            case 2:
                m_buffType = Buffs.JUMP;
            break;


        }
    }
}
