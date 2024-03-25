using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : LivingObject
{
    static public Action OnMoving;
    //Movement
    [SerializeField] protected float m_actualSpeed;
    [SerializeField] protected float m_gravity;
    [SerializeField] protected float m_jumpForce;
    [SerializeField] protected float m_sprintSpeed;

    float m_baseSpeed;

    [SerializeField] States m_actualState = States.IDLE;
 
    enum States
    {
        IDLE,
        MOVE,
        ATTACK,
        HIT,
        DIE
    }

    Vector3 m_vel;
    CharacterController m_cC;

    float m_ySpeed;

    void Start()
    {
        if (!m_cC)
        {
            m_cC = GetComponent<CharacterController>();
        }
        m_baseSpeed = m_actualSpeed;
    }

    void Update()
    {
        switch (m_actualState)
        {
            case States.IDLE:
                    Movement();
                    Attack();
                break;
            case States.MOVE:
                    Movement();
                    Attack();
                break;
            case States.ATTACK:
                break;
            case States.HIT:
                break;
            case States.DIE:
                break;
            default:
                break;
        }
        ApplyMovement();
    }

    new void Movement() //Prend les inputs et les appliques à la variable m_vel
    {
       if(m_vel != Vector3.zero)
        m_vel.x = Input.GetAxisRaw("Horizontal");
        m_vel.z = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump"))
        {
            Jump(m_jumpForce);
        }
        if(Input.GetButton("Fire3"))
        {
            SetSpeed(m_sprintSpeed);          
        }
        else
        {
            SetSpeed(m_baseSpeed);
        }
        m_actualSpeed = Mathf.Lerp(m_actualSpeed,m_actualSpeed,Time.deltaTime*2);
    }

    void ApplyMovement()
    {
        ApplyGravity();
        var direction = (transform.right * m_vel.x + transform.forward * m_vel.z).normalized;
        Vector3 finalVel = new Vector3(direction.x, m_vel.y, direction.z);
        m_cC.Move(finalVel * m_actualSpeed * Time.deltaTime);
    } //Applique les mouvements sur le Character Contrller

    void ApplyGravity()
    {
        if (!m_cC.isGrounded)
        {
            m_vel.y += m_gravity * Time.deltaTime * m_ySpeed;
            m_ySpeed += 1 * Time.deltaTime;
        }
        else
        {
            m_ySpeed = 0;
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,5,1<<6))
            {
                Debug.Log("Toucher");
                Camera.main.GetComponent<FPSCamera>().CameraShake();
            }
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward*5,Color.red);
        }
    }


    void Jump(float jumpForce)
    {
        if (!m_cC.isGrounded) return;
        m_vel.y = jumpForce;
    }

    void SetSpeed(float newSpeed)
    {
        m_actualSpeed = newSpeed;
    }
}
