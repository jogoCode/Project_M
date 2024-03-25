using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FPSCamera : MonoBehaviour
{
    Vector2 m_mouseInput;
    [SerializeField] [Range(0.1f,100)]float m_mouseSensivity;
    [SerializeField] Transform m_player;

    Camera m_camera;
    float m_rand;
   CameraStates m_actualState = CameraStates.IDLE;
    enum CameraStates
    {
        IDLE,
        SHAKE
    }

    float _vel;
    float _displacement;
    float _spring = 900;
    float _damp = 10;

    private void Start()
    {
        if (!m_player)
        {
            m_player = GetComponentInParent<PlayerController>().transform;
        }
        m_camera = GetComponent<Camera>();
    }

    void Update()
    {
        var force = -_spring * _displacement - _damp * _vel;
        _vel += force * Time.deltaTime;
        _displacement += _vel * Time.deltaTime;
        transform.position = new Vector3(transform.parent.position.x + _displacement, transform.parent.position.y+ 1.5f + _displacement, transform.parent.position.z);
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

    void ChangeState(CameraStates newState)
    {
        m_actualState = newState;
    }

    public void CameraShake()
    {
        transform.position = transform.position+transform.parent.position;
        _vel = 20f;
    }

    
 

    //TODO Changer le fov de la camera quant on sprint

}
