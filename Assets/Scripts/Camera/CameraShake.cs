using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    float _vel;
    float _displacement;
    [SerializeField][Range(0.0f, 900f)] float _spring = 300020;
    [SerializeField][Range(0.0f, 900f)] float _damp = 20;
    public IEnumerator Shake(float force , float time)
    {
        Vector3 ogPos = transform.localPosition;
        float elapsed = 0;
        _vel = force;
        while (elapsed < time)
        {
            
            var _force = -_spring * _displacement - _damp * _vel;
            _vel += _force * Time.deltaTime;
            _displacement += _vel * Time.deltaTime;
            transform.localPosition = new Vector3(ogPos.x+_displacement, ogPos.y+-_displacement, ogPos.z);
         

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
