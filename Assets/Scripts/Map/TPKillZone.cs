using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPKillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.GetComponent<PlayerController>())
            {
                other.gameObject.transform.position = new Vector3(0f,20f,0f);
            }
            else {Destroy(other.gameObject);}
        }
    }
}
