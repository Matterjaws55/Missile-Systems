using UnityEngine;

public class Sway : MonoBehaviour
{
    [Header("Sway Settings")]
    public float swayAmount = 5f;      
    public float swaySpeed = 1f;        
    public float positionSway = 0.1f;   

    private Vector3 initialRotation;
    private Vector3 initialPosition;

    void Start()
    {
        initialRotation = transform.localEulerAngles;
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.localEulerAngles = initialRotation + new Vector3(0f, 0f, sway);

        float posX = Mathf.Sin(Time.time * swaySpeed * 0.5f) * positionSway;
        float posY = Mathf.Sin(Time.time * swaySpeed * 0.3f) * positionSway;
        transform.localPosition = initialPosition + new Vector3(posX, posY, 0f);
    }
}
