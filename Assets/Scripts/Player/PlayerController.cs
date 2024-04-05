using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(StateManagable))]
public class PlayerController : LivingObject
{


    [Header("PlayerController")]
    //Movement
    CharacterController m_cC;
    Vector3 m_vel;
    [SerializeField] protected float m_actualSpeed;
    [SerializeField] protected float m_gravity;
    [SerializeField] protected float m_jumpForce;
    [SerializeField] protected float m_sprintSpeed;
    [SerializeField] float m_baseSpeed;

    StateManagable m_stateManager;
    BoxCollider m_hitBox;
    FPSCamera m_camera;


    public Levelable m_LevelSystem;
    int m_buffer = 0;


    float m_ySpeed;
    bool m_canFeedBack;
    //Actions
    public static Action<float> IsSprinting;

    public static Action End;

    new void Start()
    {
        //----------------------REFERENCES-----------------
        base.Start();
        if (!m_cC){
            m_cC = GetComponent<CharacterController>();
        }
        if (!m_LevelSystem)
        {
            m_LevelSystem = GetComponent<Levelable>();
        }
        m_hitBox = GetComponentInChildren<BoxCollider>();

        if (!m_stateManager)
        {
            m_stateManager = GetComponent<StateManagable>();
        }
        m_camera = Camera.main.GetComponent<FPSCamera>();


        //-------------------------------------------------
        m_baseSpeed = m_actualSpeed;

        //----------------------ACTIONS--------------------
        AnimationEvent.isActive += ActivateHitbox;
        AnimationEvent.isNotActive += DeActivateHitBox;

        IsSprinting?.Invoke(0);
        //End?.Invoke();
    }

    void Update()
    {
        switch (m_stateManager.GetState())
        {
            case StateManagable.States.IDLE:
                GetComponentInChildren<Animator>().speed = 1;
                Movement();           
                break;
            case StateManagable.States.ATTACK:
                Movement();
                GetComponentInChildren<Animator>().speed = m_weapon.GetAtkSpeed()/4;
                break;
        }
        if (m_stateManager.GetState() == StateManagable.States.DIE) return;
        if (Input.GetButtonDown("Fire1"))
        {
            if (!m_weapon.GetWeaponData()) return;
            m_buffer++;
            Attack();
        }
        ApplyMovement();
        UseItem();
    }

    void Movement() //Prend les inputs et les appliques à la variable m_vel
    {
        GetComponentInChildren<Animator>().speed = 1;
        m_vel.x = Input.GetAxisRaw("Horizontal");       
        m_vel.z = Input.GetAxisRaw("Vertical");
        Vector2 hVel = new Vector2(m_vel.x,m_vel.z); 
        if (Input.GetButtonDown("Jump")) // SAUT
        {
            Jump(m_jumpForce);
        }

        if (Input.GetButton("Fire3") && hVel != Vector2.zero) // SPRINT
        {
            SetSpeed(m_sprintSpeed);
            IsSprinting(m_camera.GetFOV() + m_camera.FovFactor); //change le fov de la camera
        }
        else // NORMAL
        {
            SetSpeed(m_baseSpeed);
            IsSprinting(m_camera.GetFOV() - m_camera.FovFactor);
        }
        m_actualSpeed = Mathf.Lerp(m_actualSpeed, m_actualSpeed, Time.deltaTime*2);
    }

    new public void Die(LivingObject killer)
    {
        m_stateManager.SetState(StateManagable.States.DIE);
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,75),50*Time.deltaTime);
        m_weapon.HideWeapon();
        End();        
    }

    void ApplyMovement() //Applique les mouvements sur le Character Controller
    {
        ApplyGravity();
        var direction = (transform.right * m_vel.x + transform.forward * m_vel.z).normalized;
        Vector3 finalVel = new Vector3(direction.x * m_actualSpeed, m_vel.y, direction.z * m_actualSpeed);
        m_cC.Move(finalVel* Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (!m_cC.isGrounded)
        {
            m_vel.y += m_gravity * Time.deltaTime * m_ySpeed;
            m_ySpeed += 4 * Time.deltaTime;
        }
        else
        {
            m_ySpeed = 0;
        }
    }

    //TODO ENLEVER LES GETCOMPONENT ET FAIRE UNE FONCTION POUR CHANGER LE WEAPON 
    new void Attack() 
    {
        if (!m_weapon.GetWeaponData()) return;
        if (Input.GetButtonDown("Fire1"))
        {
            m_buffer = Mathf.Clamp(m_buffer, 0, 3);
            m_stateManager.SetState(StateManagable.States.ATTACK); //change l'état en ATTACK
            m_hitBox.size = new Vector3(1, 1, m_weapon.GetRange());
            GetComponentInChildren<Animator>().SetTrigger("IsAtk");
        }
        if (m_buffer > 1)
        {
            GetComponentInChildren<Animator>().SetTrigger("IsAtk");
        }
    }

    void UseItem()
    {
        if (Input.GetButtonDown("Fire2"))  // Utilise l'item en main
        {
            if (m_weapon.GetItemData() != null)
            {
                SetHp(m_weapon.GetItemData().Hpgain);
                LifeChanged.Invoke(m_hp, m_maxHp);
                m_weapon.UseItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.A) && m_stateManager.GetState()!= StateManagable.States.ATTACK) //Lacher Item
        {
            if (m_weapon.GetWeaponData() != null || m_weapon.GetItemData() != null)
            {
                m_weapon.DropObject(true);
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Attraper Item
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit,2))
        {
            Seeitem seeitem = hit.transform.gameObject.GetComponent<Seeitem>();
            if (seeitem && Input.GetKeyDown(KeyCode.E))
            {
                seeitem.Pick(this);
            }   
        }      
    }

    void ActivateHitbox()
    {
        if (m_hitBox)
        {
            m_hitBox.enabled = true;
        }
    }

    public void DeActivateHitBox()
    {
        if (m_hitBox)
        {
            m_hitBox.enabled = false;
            m_stateManager.SetState(StateManagable.States.IDLE); //change l'état IDLE
            if (!this) return;
            StartCoroutine(ResetBuffer());
        }
    }


    void Jump(float jumpForce)
    {
        if (!m_cC.isGrounded) return;
        m_vel.y = jumpForce;
    }

    private IEnumerator ResetBuffer()
    {
        yield return new WaitForSeconds(0.1f);
        m_buffer = 0;
    }

    public override void Hit()
    {
        base.Hit();
        Camera.main.GetComponent<CameraShake>().StartCoroutine(CameraShake.cameraShake.Shake(4f, 0.5f, true, true));
        Camera.main.GetComponent<CameraShake>().StartCoroutine(CameraShake.cameraShake.Freeze(0.08f, 0.008f, true));
    }

    override protected void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(m_hp <= 0)
        {
            Die((LivingObject)other.GetComponent<EnemyController>());   
        }
        if (other.gameObject.layer == this.gameObject.layer) return;
        Hit();
    }
    //-----------------------GET-------------------------------------
    public float GetJumpForce()
    {
        return m_jumpForce;
    }

    public Levelable GetLvlSystem()
    {
        return m_LevelSystem;
    }
    //-----------------------SET-----------------------------------
    public void SetSpeed(float newSpeed)
    {
        m_actualSpeed = Mathf.Lerp(m_actualSpeed, newSpeed, (m_actualSpeed / 2) * Time.deltaTime);
    }

    public void SetJumpSpeed(float newJSpeed)
    {
        m_jumpForce += newJSpeed;
    }
    public void LoadJumpSpeed(float newJSpeed)
    {
        m_jumpForce = newJSpeed;
    }

    public void SetBaseSpeed(float newBSpeed)
    {
        m_baseSpeed += newBSpeed;
        m_sprintSpeed = m_baseSpeed + m_baseSpeed;
    }

}
