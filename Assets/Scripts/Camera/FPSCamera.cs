using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    Vector2 m_mouseInput;
    [SerializeField] [Range(0.1f,100)]float m_mouseSensivity;
    [SerializeField] Transform m_player;
    Camera m_camera;
    CameraShake m_cameraShake;
    float m_rand;
    private void Start()
    {
        if (!m_player)
        {
            m_player = GetComponentInParent<PlayerController>().transform;
        }
        m_camera = GetComponent<Camera>();
        m_cameraShake = GetComponent<CameraShake>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        m_mouseInput.x = Input.GetAxis("Mouse X") * m_mouseSensivity;
        m_mouseInput.y += Input.GetAxis("Mouse Y") * m_mouseSensivity;
        var camVRotation = m_mouseInput.y;
        m_mouseInput.y = Mathf.Clamp(m_mouseInput.y, -90, 90);
        transform.localEulerAngles = Vector3.right * -camVRotation;
        m_player.Rotate(Vector3.up * m_mouseInput.x);
    }

    //TODO AJOUTER UN FOV

}
