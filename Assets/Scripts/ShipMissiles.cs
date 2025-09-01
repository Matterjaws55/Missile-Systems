using UnityEngine;

public class ShipMissles : MonoBehaviour
{
    public Transform player;       
    public float rotationSpeed = 0.1f; 

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f; // Prevent looking up/down

        if (direction.magnitude > 0.001f)
        {
            // Target rotation only on Y axis
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate toward player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
