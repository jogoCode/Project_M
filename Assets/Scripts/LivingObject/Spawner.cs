using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemiesPrefabs;
    [SerializeField] private int _rangeDistance;
    [SerializeField] float _max;
    private DayCycleManager _cycleManager;
    [SerializeField] private bool _canSpawn = false;

    private void Start()
    {
        _cycleManager = FindObjectOfType<DayCycleManager>();
    }
    private void LateUpdate()
    {
        if((_cycleManager.IngameTime >= 19f || _cycleManager.IngameTime <= 5f) && _canSpawn == false)
        {
            _canSpawn = true;         
            StartCoroutine("CanSpawn");
        }
        if(!(_cycleManager.IngameTime >= 19f || _cycleManager.IngameTime <= 5f))
        {
            _canSpawn = false;
        }


    }
    IEnumerator CanSpawn() 
    {
        Spawn();
        Debug.Log("spawn");
        yield return new WaitForSeconds(1);
        
    }

    //SPAWN RANDOM ENEMIES RANDOM POSITION
    void Spawn()
    {

        for (int i = 0; i < _max; i++)
        {

         int randomEnemy = Random.Range(0, _enemiesPrefabs.Length);

            Instantiate(_enemiesPrefabs[randomEnemy], SetRandomPosition(), transform.rotation);
        }


    }

    Vector3 SetRandomPosition()
    {
        float randomPosX = Random.Range(-_rangeDistance, _rangeDistance);
        float randomPosZ = Random.Range(-_rangeDistance, _rangeDistance);

        Vector3 randomPosition = new Vector3(randomPosX + transform.position.x, transform.position.y, randomPosZ + transform.position.z);


        return randomPosition;
    }

 
}
