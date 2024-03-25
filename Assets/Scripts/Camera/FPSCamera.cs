using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    Vector2 m_mouseInput;
    [SerializeField] [Range(0.1f,100)]float m_mouseSensivity;
    [SerializeField] Transform m_player;

    private void Start()
    {
        if (!m_player)
        {
            m_player = GetComponentInParent<PlayerController>().transform;
        }
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        m_mouseInput.x = Input.GetAxis("Mouse X") * m_mouseSensivity;
        m_mouseInput.y = Input.GetAxis("Mouse Y") * m_mouseSensivity;
        var camVRotation = m_mouseInput.y;
        transform.localEulerAngles += Vector3.right * -camVRotation;
        camVRotation = Mathf.Clamp(camVRotation, -90, 90);
        m_player.Rotate(Vector3.up * m_mouseInput.x);
    }

    
}