using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ChooseABuff;

public class BuffButton : MonoBehaviour
{


    [SerializeField] string[] m_textsArray;
    [SerializeField] Sprite[] m_ImgArray;

    Text m_buffText;
    [SerializeField] Image m_buffImage;
    ChooseABuff.Buffs m_buffType;

    Button m_button;

    static public Action isClicked;

    void Start()
    {
        m_button = GetComponent<Button>();
        m_buffText = GetComponentInChildren<Text>();
        //m_buffImage = GetComponentInChildren<Image>();   
        m_button.onClick.AddListener(delegate { ButtonClicked(FindObjectOfType<PlayerController>().GetComponent<PlayerBuffable>());});
        InitButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitButton()
    {
        m_buffText.text = m_textsArray[(int)m_buffType];
        m_buffImage.sprite = m_ImgArray[(int)m_buffType];
        /*switch (m_buffType)
        {
            case ChooseABuff.Buffs.ATK:
                m_buffText.text = m_textsArray[(int)ChooseABuff.Buffs.ATK];
                break;
            case ChooseABuff.Buffs.HP:
                m_buffText.text = "+ HP";
                break;
            case ChooseABuff.Buffs.JUMP:
                m_buffText.text = "+ JUMP";
                break;
        }*/
    }

    void ButtonClicked(PlayerBuffable buff)
    {
        switch(m_buffType)
        {
            case ChooseABuff.Buffs.ATK:
                buff.AddAtk(5);
                break;
            case ChooseABuff.Buffs.HP:
                buff.AddHp(10);
                break;
            case ChooseABuff.Buffs.JUMP:
                buff.AddJump(5);
                break;
        }
        isClicked();
        

    }

    void SetText(string text)
    {
        m_buffText.text = text;
    }

    public void SetBuffType(ChooseABuff.Buffs newType)
    {
        m_buffType = newType;
    }

}

