using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
    public float moveSpeed = 10f;  
    private Vector3 startPosition; 

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public IEnumerator ShipHit()
    {
        float sinkDuration = 4f;      
        float sinkSpeed = 1f;          
        float elapsedTime = 0f;

        Vector3 startPos = transform.position;

        Collider col = GetComponentInChildren<Collider>();
        col.enabled = false;

        while (elapsedTime < sinkDuration)
        {
            transform.position = Vector3.Lerp(startPos, startPos + Vector3.down * sinkSpeed, elapsedTime / sinkDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}