using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : LivingObject
{


    [Header("PlayerController")]
    //Movement
    [SerializeField] protected float m_actualSpeed;
    [SerializeField] protected float m_gravity;
    [SerializeField] protected float m_jumpForce;
    [SerializeField] protected float m_sprintSpeed;
    float m_baseSpeed;
    BoxCollider m_hitBox;
    [SerializeField] States m_actualState = States.IDLE;

    public Levelable m_LevelSystem;
    int m_buffer = 0;

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
    bool m_canFeedBack;

    void Start()
    {
        base.Start();
        if (!m_cC){
            m_cC = GetComponent<CharacterController>();
        }
        if (!m_LevelSystem)
        {
            m_LevelSystem = GetComponent<Levelable>();
        }

        m_baseSpeed = m_actualSpeed;
        m_hitBox = GetComponentInChildren<BoxCollider>();
        AnimationEvent.isActive += ActivateHitbox;
        AnimationEvent.isNotActive += DeActivateHitBox;
    }

    void Update()
    {
        switch (m_actualState)
        {
            case States.IDLE:
                GetComponentInChildren<Animator>().speed = 1;
                Movement();
                Attack();
                break;
            case States.MOVE:
                Movement();
                Attack();
                break;
            case States.ATTACK:
                Movement();
                GetComponentInChildren<Animator>().speed = m_weapon.AtkSpeed / 2;
                break;
            case States.HIT:
                break;
            case States.DIE:
                break;
            default:
                break;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            m_buffer++;
        }
        ApplyMovement();

    }

    void Movement() //Prend les inputs et les appliques à la variable m_vel
    {
        GetComponentInChildren<Animator>().speed = 1;
        if (m_vel != Vector3.zero)
            m_vel.x = Input.GetAxisRaw("Horizontal");
        m_vel.z = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump"))
        {
            Jump(m_jumpForce);
        }
        if (Input.GetButton("Fire3"))
        {
            SetSpeed(m_sprintSpeed);
        }
        else
        {
            SetSpeed(m_baseSpeed);
        }
        m_actualSpeed = Mathf.Lerp(m_actualSpeed, m_actualSpeed, Time.deltaTime * 2);
    }

    void ApplyMovement() //Applique les mouvements sur le Character Contrller
    {
        ApplyGravity();
        var direction = (transform.right * m_vel.x + transform.forward * m_vel.z).normalized;
        Vector3 finalVel = new Vector3(direction.x, m_vel.y, direction.z);
        m_cC.Move(finalVel * m_actualSpeed * Time.deltaTime);
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

    //TODO ENLEVER LES GETCOMPONENT ET FAIRE UNE FONCTION POUR CHANGER LE WEAPON 
    new void Attack() 
    {

        if (Input.GetButtonDown("Fire1"))
        {
            m_buffer = Mathf.Clamp(m_buffer, 0, 3);
            m_actualState = States.ATTACK;
            m_hitBox.size = new Vector3(1, 1, m_weapon.Range);
            GetComponentInChildren<Animator>().SetTrigger("IsAtk");
        }
        if (m_buffer > 1)
        {
            GetComponentInChildren<Animator>().SetTrigger("IsAtk");
        }
    }

    void ActivateHitbox()
    {
        if (m_hitBox)
        {
            m_hitBox.enabled = true;
        }
    }

    void DeActivateHitBox()
    {
        if (m_hitBox)
        {
            m_hitBox.enabled = false;
            m_actualState = States.IDLE;
            if (!this) return;
            StartCoroutine(ResetBuffer());
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

    private IEnumerator ResetBuffer()
    {
        yield return new WaitForSeconds(0.1f);
        m_buffer = 0;
    }

    override protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.layer == this.gameObject.layer) return;
        //FEEdBACK
        Camera.main.GetComponent<CameraShake>().StartCoroutine(CameraShake.cameraShake.Shake(4f, 0.5f, true, true));
        Camera.main.GetComponent<CameraShake>().StartCoroutine(Camera.main.GetComponent<CameraShake>().Freeze(0.08f, 0.008f));
        //PARTICLE
        Instantiate(m_hitFx, transform.position, Quaternion.identity);
    }

}
