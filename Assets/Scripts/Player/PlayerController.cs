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
    [SerializeField] protected float m_speed;
    [SerializeField] protected float m_gravity;
    [SerializeField] protected float m_jumpForce;
    
    
    Vector3 m_vel;
    CharacterController m_cC;

    float m_ySpeed;

    void Start()
    {
        if (!m_cC)
        {
            m_cC = GetComponent<CharacterController>();
        } 
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
       if(m_vel != Vector3.zero)
        m_vel.x = Input.GetAxisRaw("Horizontal");
        m_vel.z = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump"))
        {
            Jump(m_jumpForce);
        }
        ApplyGravity();
        var direction = (transform.right * m_vel.x + transform.forward * m_vel.z).normalized;
        Vector3 finalVel = new Vector3(direction.x, m_vel.y,direction.z);
        m_cC.Move(finalVel*m_speed*Time.deltaTime);
    }




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


    void Jump(float jumpForce)
    {
        m_vel.y = jumpForce;
    }
}
