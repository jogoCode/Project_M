using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : LivingObject
{
    [Header("EnemyController")]
    //MOVEMENT 
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotSpeed;

    [SerializeField] private NavMeshAgent _agent;

    //DETECT
    [SerializeField] private int _radiusR;
    [SerializeField] private Transform _sphereR;


    void Start()
    {
        if (_agent == null)
        {
            return;      
        }
    }

    private void Update()
    {
        //LIFE
       // Die();
        if (m_hp <= 0)
        {
            Destroy(gameObject);
        _agent.enabled = false;
        }

        //DETECT AND MOVE
        MoveTowardsPlayer();

    }

    public void MoveTowardsPlayer()
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

                    Vector3 targetPos = _target.position;
                    targetPos.y = transform.position.y;


                    Vector3 dir = (targetPos - transform.position).normalized;

        //TURN SLOWLY
                    Quaternion targetRot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, _rotSpeed * Time.deltaTime);

        //MOVE
                    
                    _agent.SetDestination(_target.position);
                }

            }
        }
    }


    //TAKE DAMAGE IF DEFENSE
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
            //SetHp(-1);
            }

        }
    }

 
    //DETECTION RADIUS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_sphereR.position, _radiusR);
    }
    
}
