using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 15f;
    public float maxRotation = 50;
    private float speed = 0;
    private float currentRotation = 0f; 
    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if (speed != 0)
        {
            float deltaRotation = speed * Time.deltaTime;
            float newRotation = Mathf.Clamp(currentRotation + deltaRotation, -maxRotation, maxRotation);
            float clampedDelta = newRotation - currentRotation;
            transform.Rotate(Vector3.up, clampedDelta, Space.Self);

            currentRotation = newRotation;
        }
    }

    public void TurnRight()
    { speed = -rotationSpeed; }

    public void TurnLeft()
    { speed = rotationSpeed; }

    public void ButtonUp()
    { speed = 0; }
}
