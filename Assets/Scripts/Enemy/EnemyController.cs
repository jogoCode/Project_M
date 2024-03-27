using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : LivingObject
{
    [Header("EnemyController")]
    //MOVEMENT 
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected Transform _target;
    [SerializeField] private float _rotSpeed;

    [SerializeField] private NavMeshAgent _agent;

    //DETECT
    [SerializeField] private int _radiusR;
    [SerializeField] private Transform _sphereR;


    protected virtual void Start()
    {

        if (_agent == null)
        {
            return;      
        }
        
        _target = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    private void Update()
    {
        //LIFE

        //DETECT AND MOVE
        _agent.speed = _moveSpeed;
        MoveTowardsPlayer();

    }

    protected void MoveTowardsPlayer()
    {
        //DETECT PLAYER
        Collider[] player = Physics.OverlapSphere(_sphereR.position, _radiusR);
        foreach (Collider detection in player)
        {

            if (detection.GetComponent<PlayerController>() != null)
            {

        //TAKE THE DIRECTION
                    if (_target != null)
                    {
                        //Debug.Log("shoot");                 
                        EnemyShoot.OnShoot();

                        Vector3 targetPos = _target.position;
                        targetPos.y = transform.position.y;


                        Vector3 dir = (targetPos - transform.position).normalized;

        //TURN SLOWLY
                        Quaternion targetRot = Quaternion.LookRotation(dir);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, _rotSpeed * Time.deltaTime);

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

            }
        }
   
    }


    //MAKE DAMAGE IF DEFENSE
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
        {
            if (m_armor >= collision.gameObject.GetComponent<PlayerController>().GetArmor())
            {
            collision.gameObject.GetComponent<PlayerController>().SetHp(-1);
            }
            else
            {
            SetHp(-1);
            }

        }
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
        Gizmos.DrawWireSphere(_sphereR.position, _radiusR);
    }
    
}
