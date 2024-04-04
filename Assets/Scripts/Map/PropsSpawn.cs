using UnityEngine;

public class PropsSpawn : MonoBehaviour
{
    private void Start()
    {
        
        RaycastHit hit;
        float RandoRota = Random.Range(0f, 360f);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100))
        {
            transform.position = hit.point;
            Vector3 angle = Vector3.Lerp(hit.normal, Vector3.up, 0.5f);
            transform.up = angle;
            transform.Rotate(Vector3.up,RandoRota);
        }
    }
}
