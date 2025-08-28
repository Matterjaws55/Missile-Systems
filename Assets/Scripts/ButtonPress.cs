using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour
{
    public enum ButtonFunction
    {
        StartGame,
        ExitGame,
        DoesNothing
    }

    [Header("Button Type")]
    public ButtonFunction function;

    public float moveDistance = 0.02f;  
    public float moveSpeed = 1f;     
    private Vector3 originalPosition;
    private bool isReturning = false;

    public ParticleSystem sparksParticle;

    AudioSource audio;


    void Start()
    {
        originalPosition = transform.localPosition;

        audio = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        if (!isReturning)
            StartCoroutine(PressDownUp());

        switch (function)
        {
            case ButtonFunction.StartGame:
                StartGame();
                break;

            case ButtonFunction.ExitGame:
                ExitGame();
                break;

            case ButtonFunction.DoesNothing:
                Debug.Log("Does nothing.");
                break;
        }
    }

    void StartGame()
    {
        ShipSpawner spawner = GetComponent<ShipSpawner>();
        spawner.GameStart();

        Debug.Log("Game Start");
    }

    void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator PressDownUp()
    {
        audio.Play();
        sparksParticle.Play();

        Vector3 downOffset = transform.TransformDirection(new Vector3(0, -moveDistance, 0));
        Vector3 downPosition = originalPosition + transform.InverseTransformDirection(downOffset);

        while (Vector3.Distance(transform.localPosition, downPosition) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, downPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f); 

        isReturning = true;

        while (Vector3.Distance(transform.localPosition, originalPosition) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }

        transform.localPosition = originalPosition; 
        isReturning = false;
        sparksParticle.Stop();
    }
}