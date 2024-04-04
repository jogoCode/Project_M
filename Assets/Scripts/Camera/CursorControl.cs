using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    bool m_isLock = true;

    private void Start()
    {
        PlayerController.End += DisableLock;
        Levelable.isLevelUp += DisableLock;
        BuffButton.isClicked += EnableLock;
    }

    private void Update()
    {
        if(m_isLock == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (m_isLock == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void DisableLock()
    {
        m_isLock = false;
    }

    public void EnableLock()
    {
        m_isLock = true;
    }
}
