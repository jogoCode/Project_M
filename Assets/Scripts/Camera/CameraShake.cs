using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{



    public IEnumerator Shake(float force , float time)
    {
        Vector3 ogPos = transform.localPosition;
        float elapsed = 0; 
        while (elapsed < time)
        {
            float x = Random.Range(-0.5f, 0.5f) * force;
            float y = Random.Range(-0.5f, 0.5f) * force;
            transform.localPosition =  new Vector3(ogPos.x, y + 1.753f, ogPos.z);
            elapsed += Time.deltaTime;
            //Time.timeScale = 0.1f;
            yield return null;
        }  
        transform.localPosition = ogPos;
        Time.timeScale = 1f;
    }
}
