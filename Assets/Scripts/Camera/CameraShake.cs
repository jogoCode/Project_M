using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  CameraShake : MonoBehaviour
{
    float _vel;
    float _displacement;
    [SerializeField][Range(0.0f, 900f)] float _spring;
    [SerializeField][Range(0.0f, 900f)] float _damp;
    static public CameraShake cameraShake;
    private void Start()
    {
        cameraShake = GetComponent<CameraShake>();
    }

    public IEnumerator Shake(float force , float time,bool x, bool y)
    {
        Vector3 ogPos = transform.localPosition;
        float elapsed = 0;
        _vel = force;
        while (elapsed < time)
        {
            
            var _force = -_spring * _displacement - _damp * _vel;
            _vel += _force * Time.deltaTime;
            _displacement += _vel * Time.deltaTime;


            var h = ogPos.x;
            var v = ogPos.y;
            if (x && y) {
                h = ogPos.x + _displacement;
                v = ogPos.y + _displacement;
            }
            if (x && !y)
            {
                h = ogPos.x + _displacement;
            }
            if (!x && y)
            {
                v = ogPos.y + _displacement;
            }
            transform.localPosition = new Vector3(h, v, ogPos.z);
         

            elapsed += Time.deltaTime;
            yield return null;
        }  
        transform.localPosition = ogPos;
        
    }

    public IEnumerator Freeze(float time,float duration)
    {
        Time.timeScale = time;
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1;
    }
}
