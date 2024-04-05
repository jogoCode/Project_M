using System.Collections;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private Transform _water;
    [SerializeField] private bool _waterReach = false;

    private void Update()
    {
        if ( _water.position.y <= 0.35f && _waterReach == false )
        {
            for (int i = 0; i < 50; i++)
            {
                _water.position = new Vector3(_water.position.x, _water.position.y + Random.Range(0.0003f,0.0001f) * Time.deltaTime);
            }
        }
        else if (_water.position.y >= 0.40f || _waterReach == true)
        {
            for(int i = 0;i < 50;i++) 
            { 
                _water.position = new Vector3(_water.position.x, _water.position.y - Random.Range(0.0003f, 0.0001f) * Time.deltaTime);
            }
        }

        if (_water.position.y > 0.35)
        {
            _waterReach = true;
        }

        if( _water.position.y < 0.3)
        {
            _waterReach = false;
        }

    }
}
