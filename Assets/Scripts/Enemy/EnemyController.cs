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
    [SerializeField] private int _radius;
    [SerializeField] private Transform _sphere;

    //RANDOM MOVE
    [SerializeField] private Transform _fakeTarget;
    [SerializeField] private float _rangeDistance;
    [SerializeField] private float _delayChangePos;

    //SHOOT
    [SerializeField] protected bool _isShooting;

    [SerializeField] private Transform _boxDetect;
    [SerializeField] private Vector3 _radBox;

    [SerializeField] private GameObject _enemies;
    [SerializeField] private bool _canSpawn;


    protected override void Start()
    {
        base.Start();

        _target = FindObjectOfType<PlayerController>().gameObject.transform;

        //RANDOM MOVE
        Move();

        if (_agent == null)
        {
            _agent = GetComponent<NavMeshAgent>();      
        }



    }

    private void Update()
    {
        //LIFE
        Die();

        //DETECT AND MOVE
        _agent.speed = _moveSpeed;
        MoveTowardsPlayer();

        //SHOOT
        if (_boxDetect != null)
        {
        Distance();
        }
        else
        {
        return; 
        }

        if (_canSpawn == true)
        {
            if (m_hp <= m_maxhp / 4)
            {
                CallEnemies();
                _canSpawn = false;
            }
        }

    }


    void CallEnemies()
    {
        for (int i = 0; i < 2; i++)
        {
            
        if (_enemies != null)
        {
                Instantiate(_enemies, transform.position, transform.rotation);
  
        }         
        }

    }
    //SHOOT AT LONG DISTANCE
    void Distance()
    {
        Collider[] player = Physics.OverlapBox(_boxDetect.position, _radBox/2);
        foreach (Collider detection in player)
        {
            if (detection.GetComponent<PlayerController>() != null)
            {
                _isShooting = true;

            }
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

       StartCoroutine(randomTarget());

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
   _agent.SetDestination(_fakeTarget.position);
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
                    //TAKE THE DIRECTION
                    if (_target != null)
                    {

                        //Debug.Log("shoot");                 
                        //EnemyShoot.OnShoot();
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
    //MAKE DAMAGE IF DEFENSE
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {

            if (m_armor >= collision.gameObject.GetComponent<PlayerController>().GetArmor())
            {
            collision.gameObject.GetComponent<PlayerController>().SetHp(-5);
                _canSpawn = true;
            }

        }
    }

    //PLAYER LEAVES THE SPHERE
    private void OnTriggerExit(Collider other)
    {
        _isShooting = false;
    }

    //TAKE DAMAGE
    public void TakeDamage()
    {
        SetHp(-1);
    }

 
    //DETECTION RADIUS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_sphere.position, _radius);

        if (_boxDetect == null)
        { 
            return;
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_boxDetect.position, _radBox);
        }
    }
    
}
