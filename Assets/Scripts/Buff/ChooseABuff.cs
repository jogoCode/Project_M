using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ChooseABuff : MonoBehaviour
{

    PlayerController m_playerTarget;
    [SerializeField] GameObject m_buffButton;
    [SerializeField] Vector3 m_displayPos;
    [SerializeField] float m_displayOffset;
    [SerializeField] GameObject m_buffsParent;


    Buffs m_buffType;
    public enum Buffs
    {
        ATK,
        HP,
        JUMP,
        SPEED,
        XPBOOST,
        ARMOR,
        DASH
    }

    bool m_canDisplay = true;
    public void Start()
    {
        if (!m_buffButton)
        {
            Debug.LogError("Il manque le préfabs du buffButton "+this.gameObject);
        }
        //UpdateBuff();
        BuffButton.isClicked += RemoveBuffs;
        Levelable.isLevelUp += UpdateBuff;
    }

    public void UpdateBuff()
    {
        if (m_canDisplay != true) return;
        m_canDisplay=false;
        Vector2 camSize = new Vector2(Camera.main.pixelRect.width, Camera.main.pixelRect.height);
        for (int i = 0; i< 2; i++)
        {
            var buffPrefab = Instantiate(m_buffButton,new Vector3(m_buffsParent.transform.position.x + i * m_displayOffset , m_displayPos.y, m_displayPos.z), Quaternion.identity,m_buffsParent.transform);
            var buff = buffPrefab.GetComponent<BuffButton>();
            buff.SetBuffType(RandBuff());
        }
        m_canDisplay = true;
        Debug.Log(Camera.main.pixelRect.width);
    }


    public void RemoveBuffs()
    {
        foreach (Transform child in m_buffsParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public Buffs RandBuff()
    {
        var rand = Random.Range(0,3);
        switch(rand){ 
            case 0:
                return Buffs.ATK;
            case 1:
                return Buffs.HP;
            case 2: 
                return Buffs.JUMP; 
        }
        return Buffs.ATK;
    }
}
