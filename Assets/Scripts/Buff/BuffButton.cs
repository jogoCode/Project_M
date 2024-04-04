using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffButton : MonoBehaviour
{


    [SerializeField] string[] m_textsArray;
    Text m_buffText;
    Image m_buffImage;
    void Start()
    {
        m_buffText = GetComponent<Text>();
        m_buffImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetText(string text)
    {
        m_buffText.text = text;
    }
}
