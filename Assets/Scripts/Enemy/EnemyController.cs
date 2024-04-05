using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController : LivingObject
{
    [Header("EnemyController")]
    //MOVEMENT 
    [SerializeField] protected float _moveSpeed;
    [SerializeField] private float _rotSpeed;
    protected Transform _target;

    protected NavMeshAgent _agent;

    //DETECT
    [SerializeField] protected float _radius;

    //RANDOM MOVE
    [SerializeField] private Transform _fakeTarget;
    [SerializeField] private float _rangeDistance;
    [SerializeField] private float _delayChangePos;

    //SHOOT
    protected bool _isShooting;

    private Rigidbody _rb;

    [SerializeField] private Vector3 _position;

    private bool _isMoreDistanced;

 
    protected override void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        base.Start();

        _target = FindObjectOfType<PlayerController>().gameObject.transform;
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        
        //Faire un rayon vers le bas qui est capable de voir le terrain
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down);
        
        //Si le rayon touche on recupere les coordonnees, ca devient le transform position de notre agent
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100))
        {
            if (hit.collider != null)
            {
                _agent.Warp(hit.point); // tp l'agent
            }
        }



        _agent.speed = _moveSpeed;

        _isMoreDistanced = false;

            //RANDOM MOVE
            Move();

        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();      
        }
    }



    protected virtual void Update()
    {       
        //DETECT AND MOVE

        MoveTowardsPlayer();
        
    }
    public void Recoil()
    {
        StartCoroutine(recoilTime());
        IEnumerator recoilTime()
        {
            _rb.AddForce(-transform.forward * (m_weapon.GetWeaponData().KnockBack), ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
            _rb.velocity = Vector3.zero;
        }
    }
    
    
    //MOVE RANDOM
   private void Move()
    {
        if (_fakeTarget != null)
        {
        _agent.SetDestination(_fakeTarget.position);

        }
        else
       { 
       return; 
       }

        if(_agent != null)
        {
       StartCoroutine(randomTarget());

        }

    } 
   Vector3 SetRandomPosition()
   {
   float randomPosX = Random.Range(-_rangeDistance, _rangeDistance);
   float randomPosZ = Random.Range(-_rangeDistance, _rangeDistance);

   Vector3 randomPosition = new Vector3(randomPosX+ transform.position.x, _position.y, randomPosZ+ transform.position.z);

   return randomPosition;
   }
   IEnumerator randomTarget()
   {
   while (true)
   {
                SetRandomPosition();
                _fakeTarget.position = SetRandomPosition();
            if (_agent != null)
            {
                if (_agent.enabled == true)
                {
                    _agent.SetDestination(_fakeTarget.position);
                }
            }
                yield return new WaitForSeconds(_delayChangePos);
   }
 
   }

    //MOVE TOWARDS PLAYER
    protected virtual void MoveTowardsPlayer()
    {
        //DETECT PLAYER
        Collider[] player = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider detection in player)
        {
            if (detection.GetComponent<PlayerController>() != null)
            {
                if (_agent != null)
                {
        //TAKE THE DIRECTION
                    if (_target != null)
                    {
                        _isShooting = true;

                        Vector3 targetPos = _target.position;
                        targetPos.y = transform.position.y;


                        Vector3 dir = (targetPos - transform.position).normalized;

        //TURN SLOWLY
                        Quaternion targetRot = Quaternion.LookRotation(dir);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, _rotSpeed * Time.deltaTime);

                        PlayerDetected();                    

                    }
                }
            }
        }
    }

    //MOVETOPLAYER
    public void PlayerDetected()
    {
        //MOVE IF AGENTNAVMESHACTIV
        if (_agent.enabled == true)
        {
            _agent.SetDestination(_target.position);
        }
        else
        {
            _agent.enabled = false;
        }

    }

    //MAKE DAMAGE 
    protected override void OnTriggerEnter(Collider other)
    {
        var playerState = other.GetComponentInParent<StateManagable>();
        if (!playerState) return;// Verife si le joueur 
        if (playerState.GetState() != StateManagable.States.ATTACK) return;
        playerState.GetComponent<PlayerController>().DeActivateHitBox();
        base.OnTriggerEnter(other);

        var player = other.GetComponentInParent<PlayerController>();
        if (player != null)
        {
                Hit();
                _rb.constraints = RigidbodyConstraints.None;
                if (_isMoreDistanced == false)
                {               
                     _radius += player.GetComponentInParent<WeaponManager>().GetWeaponData().KnockBack;
                    _isMoreDistanced = true;
                }
                

            if (m_hp <= 0)
            {
                Die(player);             
            }
            if (other.gameObject.layer == 1 << 7) return;  // Verifie Si c'est un joueur ou non pour appliquer les feedsBck
                                                           // CAMERA SHAKE ET FREEZE

            Camera.main.GetComponent<CameraShake>().StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(5f, 0.5f, true, false));
            Camera.main.GetComponent<CameraShake>().StartCoroutine(Camera.main.GetComponent<CameraShake>().Freeze(0.1f, 0.008f, false));
        }
       

    }

    IEnumerator Hitwait()
    {
        if(_agent != null)
        {
            //_agent.enabled = false;  
            yield return new WaitForSeconds(0.5f);
            _rb.velocity = Vector3.zero;
            _agent.enabled = true;
        }
    }

    //TAKE DAMAGE
    public override void Hit()
    {
        base.Hit();
        Recoil();
        StartCoroutine(Hitwait());    
        Debug.Log("IsHit");
    }

    //PLAYER LEAVES THE SPHERE
    private void OnTriggerExit(Collider other)
    {
        _isShooting = false;
    }

    new public void Die(LivingObject killer)
    {
        if (killer.GetComponent<PlayerController>()) 
        {
            var player = killer.GetComponent<PlayerController>();
            player.m_LevelSystem.AddExp(5);
            IsDying?.Invoke(player.m_LevelSystem.GetExp(), player.m_LevelSystem.GetMaxExp());
            gameObject.SetActive(false);
            Destroy(gameObject,30);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    //DETECTION RADIUS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }


   
    
}
