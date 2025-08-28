using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HomingMissile : MonoBehaviour
{
    [Header("Missile Settings")]
    public float speed = 20f;                  
    public float rotationSpeed = 5f;           
    public float predictionFactor = 1.5f;      
    public float closeDistance = 10f;         
    public float wobbleMagnitude = 0.5f;      
    public float wobbleFrequency = 2f;        

    [Header("Target")]
    public float hoverTimeToLock = 1.5f;
    private Transform target;
    private float hoverTimer = 0f;
    private Transform hoveredTarget = null;

    [Header("Reticle")]
    public RectTransform reticleUI;
    public Image reticleImage;
    public Color normalColor = Color.white;
    public Color lockedColor = Color.red;
    public GameObject lockOnText;

    [Header("Visual & Audio")]
    public GameObject missileModel;           
    public ParticleSystem smokeTrail;          
    public ParticleSystem explosionEffect;  
    public AudioSource missileSound;       
    public AudioSource explosionSound;

    [Header("Scripts")]
    public CameraShake camShake;
    public CrankEffect reload;

    private Rigidbody rb;
    private Vector3 wobbleOffset;
    string targetTag = "Target";
    private bool lockedOn = false;
    private bool launched = false;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private float tempSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        HandleTargeting();

        if (lockedOn && reload.hasReloaded && !launched && Input.GetKeyDown(KeyCode.Return))
        {
            launched = true;
            missileSound.Play();
            StartCoroutine(camShake.Shaking());
            reload.hasReloaded = false;
        }
    }

    void FixedUpdate()
    {
        if (!launched || target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        Vector3 targetVelocity = Vector3.zero;
        Rigidbody targetRb = target.GetComponent<Rigidbody>();
        targetVelocity = targetRb.linearVelocity;

        Vector3 aimPoint;
        if (distance > closeDistance) aimPoint = target.position + targetVelocity * predictionFactor;
        else
            aimPoint = target.position;

        wobbleOffset = new Vector3(Mathf.Cos(Time.time * wobbleFrequency), Mathf.Sin(Time.time * wobbleFrequency * 0.7f), Mathf.Cos(Time.time * wobbleFrequency * 1.3f)) * wobbleMagnitude;

        Vector3 desiredDirection = (aimPoint - transform.position).normalized + wobbleOffset;

        Quaternion desiredRotation = Quaternion.LookRotation(desiredDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.fixedDeltaTime);

        rb.linearVelocity = transform.forward * speed;
    }

    private void HandleTargeting()
    {
        Vector3[] corners = new Vector3[4];
        reticleUI.GetWorldCorners(corners);

        float minX = corners[0].x;
        float maxX = corners[2].x;
        float minY = corners[0].y;
        float maxY = corners[2].y;
        Rect screenRect = new Rect(minX, minY, maxX - minX, maxY - minY);

        Collider[] allTargets = GameObject.FindGameObjectsWithTag(targetTag).Select(t => t.GetComponent<Collider>()).Where(c => c != null).ToArray();

        bool hoveringOverTarget = false;

        if (!lockedOn)
        {
            foreach (var col in allTargets)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(col.bounds.center);

                if (screenRect.Contains(screenPos, true))
                {
                    hoveringOverTarget = true;

                    if (hoveredTarget == col.transform)
                    {
                        hoverTimer += Time.deltaTime;
                        if (hoverTimer >= hoverTimeToLock)
                        {
                            target = hoveredTarget;
                            lockedOn = true;
                            hoverTimer = 0f;
                        }
                    }
                    else
                    {
                        hoveredTarget = col.transform;
                        hoverTimer = 0f;
                    }

                    break; 
                }
            }
        }
        else
        {
            foreach (var col in allTargets)
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(col.bounds.center);
                if (screenRect.Contains(screenPos, true) && col.transform == target)
                {
                    hoveringOverTarget = true;
                    break;
                }
            }
        }

        if (hoveringOverTarget)
        {
            if (!lockedOn)
            {
                reticleImage.color = Color.Lerp(normalColor, lockedColor, hoverTimer / hoverTimeToLock);
                lockOnText.SetActive(false);
            }
            else
            {
                reticleImage.color = lockedColor;
                lockOnText.SetActive(true);
            }
        }
        else
        {
            reticleImage.color = normalColor;
            lockOnText.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform == target)
        {
            ShipMovement ship = other.gameObject.GetComponentInParent<ShipMovement>();

            if (ship != null)
            {
                ship.StartCoroutine(ship.ShipHit());
            }

            Explode();
        }
    }

    void Explode()
    {
        tempSpeed = speed;
        speed = 0;
        missileModel.SetActive(false);
        smokeTrail.Stop();
        explosionEffect.Play();
        missileSound.Stop();
        explosionSound.Play();
        StartCoroutine(camShake.Shaking());

        StartCoroutine(ResetMissile());
    }

    private IEnumerator ResetMissile()
    {
        yield return new WaitForSeconds(2f);

        speed = tempSpeed;

        transform.position = startPosition;
        transform.rotation = startRotation;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        missileModel.SetActive(true);
        smokeTrail.Play();
        explosionEffect.Stop();

        target = null;
        hoveredTarget = null;
        hoverTimer = 0f;
        lockedOn = false;
        launched = false;

        reticleImage.color = normalColor;
        lockOnText.SetActive(false);
    }

        void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
