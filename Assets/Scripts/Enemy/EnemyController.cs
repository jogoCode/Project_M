using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController : LivingObject
{
    [Header("EnemyController")]
    //MOVEMENT 
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected Transform _target;
    [SerializeField] private float _rotSpeed;

    [SerializeField] private NavMeshAgent _agent;

    //DETECT
    [SerializeField] protected int _radius;
    [SerializeField] protected Transform _sphere;

    //RANDOM MOVE
    [SerializeField] private Transform _fakeTarget;
    [SerializeField] private float _rangeDistance;
    [SerializeField] private float _delayChangePos;

    //SHOOT
    [SerializeField] protected bool _isShooting;

    [SerializeField] private float _recoil;
    [SerializeField] private Rigidbody _rb;

 
    protected override void Start()
    {
        base.Start();

        _target = FindObjectOfType<PlayerController>().gameObject.transform;
        _rb = GetComponent<Rigidbody>();

        //RANDOM MOVE
        Move();

        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();      
        }

    }

    protected virtual void Update()
    {
        //LIFE
        Die();

        //DETECT AND MOVE
        _agent.speed = _moveSpeed;
        MoveTowardsPlayer();

       
    }
    public void Recoil()
    {
        StartCoroutine(recoilTime());
        IEnumerator recoilTime()
        {
            _rb.AddForce(-transform.forward * (m_weapon.KnockBack), ForceMode.Impulse);
            yield return new WaitForSeconds(1f);
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

   Vector3 randomPosition = new Vector3(randomPosX+ transform.position.x, transform.position.y, randomPosZ+ transform.position.z);

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
                _agent.SetDestination(_fakeTarget.position);
            }
                yield return new WaitForSeconds(_delayChangePos);
   }
 
   }

    protected virtual void MoveTowardsPlayer()
    {
        //DETECT PLAYER
        Collider[] player = Physics.OverlapSphere(_sphere.position, _radius);
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
            //_agent.SetDestination(-_target.position); EVITER LE PLAYER
            _agent.SetDestination(_target.position);
        }
        else
        {
            _agent.enabled = false;
        }

    }
    //MAKE DAMAGE IF DEFENSE
    protected override void OnTriggerEnter(Collider other)
    {
            base.OnTriggerEnter(other);

            Hit();
            Debug.Log("ok");
    }

    IEnumerator Hitwait()
    {
        if(_agent != null)
        {
        _agent.enabled = false;
        yield return new WaitForSeconds(0.75f);
        _agent.enabled = true;
        }
    }

    //TAKE DAMAGE
    new public void Hit()
    {
        StartCoroutine(Hitwait());
        Recoil();
        Debug.Log("IsHit");
    }

    //PLAYER LEAVES THE SPHERE
    private void OnTriggerExit(Collider other)
    {
        _isShooting = false;
    }


    //DETECTION RADIUS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_sphere.position, _radius);
    }
    
}
