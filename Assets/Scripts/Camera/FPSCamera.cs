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
    CameraShake m_cameraShake;

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
        m_cameraShake = GetComponent<CameraShake>();
    }

    void Update()
    {
        var force = -_spring * _displacement - _damp * _vel;
        _vel += force * Time.deltaTime;
        _displacement += _vel * Time.deltaTime;
        var direction = transform.right + transform.forward;
        transform.position = new Vector3(transform.parent.position.x + -_displacement, transform.position.y, transform.parent.position.z);
        RotateCamera();
    }

    void RotateCamera()
    {
        m_mouseInput.x = Input.GetAxis("Mouse X") * m_mouseSensivity;
        m_mouseInput.y = Input.GetAxis("Mouse Y") * m_mouseSensivity;
        var camVRotation = m_mouseInput.y;
        transform.localEulerAngles += Vector3.right * -camVRotation;
        Debug.Log(camVRotation);
        m_player.Rotate(Vector3.up * m_mouseInput.x);
        camVRotation = Mathf.Clamp(camVRotation, -90, 90);
    }

    void ChangeState(CameraStates newState)
    {
        m_actualState = newState;
    }

    public IEnumerator Shake(float force, float time)
    {
        Vector3 ogPos = transform.localPosition;
        float elapsed = 0;
        while (elapsed < time)
        {
            float x = Random.Range(-1, 1) * force;
            float y = Random.Range(-1, 1) * force;
            transform.localPosition = new Vector3(x, y+transform.parent.position.y, ogPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = ogPos;
    }



    //TODO Changer le fov de la camera quant on sprint
    //TODO Finir le camera shake;

}
