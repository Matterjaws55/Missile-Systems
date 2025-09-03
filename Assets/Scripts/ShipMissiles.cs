using UnityEngine;

public class ShipMissles : MonoBehaviour
{
    public Transform player;       
    public float rotationSpeed = 10f; 

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
