using System;
using UnityEngine;

[Serializable]
public class TodoElement
{
    [SerializeField] public bool m_completed = false;
    [SerializeField] public string m_title;
    [SerializeField] public string m_description;

    public TodoElement(string title, string description = "", bool completed = false)
    {
        m_title = title;
        m_description = description;
        m_completed = completed;
    }
}
