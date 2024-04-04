using UnityEngine;

public class ChestSpawn : MonoBehaviour
{
    private void Start()
    {
        RaycastHit hit;
        float RandoRota = Random.Range(0f, 360f);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 100))
        {
            transform.position = hit.point;
            transform.up = hit.normal;
            transform.Rotate(Vector3.up, RandoRota);
        }
    }
}
