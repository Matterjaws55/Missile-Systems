using UnityEngine;
using UnityEngine.EventSystems;

public class CrankEffect : MonoBehaviour
{
    public float rotationSpeed = 0.5f;   
    public AudioSource crankSound;
    public AudioSource reloadSound;

    private bool isDragging = false;
    private float lastAngle;
    private float accumulatedRotation = 0f;
    public bool hasReloaded = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    isDragging = true;
                    lastAngle = GetMouseAngle();

                    crankSound.Play();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            crankSound.Stop();
        }

        if (isDragging)
        {
            float currentAngle = GetMouseAngle();
            float deltaAngle = Mathf.DeltaAngle(lastAngle, currentAngle);
            float appliedRotation = deltaAngle * -rotationSpeed;

            if (appliedRotation < 0f)
            {
                transform.Rotate(Vector3.up, appliedRotation, Space.World);
                accumulatedRotation += appliedRotation;

                while (accumulatedRotation <= -1080)
                {
                    accumulatedRotation += 1080;
                    hasReloaded = true;

                    reloadSound.Play();
                }
            }

            lastAngle = currentAngle;
        }
    }

    private float GetMouseAngle()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 dir = (Vector2)(Input.mousePosition - screenPos);
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }
}