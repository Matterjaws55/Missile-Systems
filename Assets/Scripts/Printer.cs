using UnityEngine;
using System.Collections;
using UnityEngine.ProBuilder;
using UnityEngine.XR;

public class Printer : MonoBehaviour
{
    public GameObject tutorialUI;
    public GameObject model;
    public Animator paper;
    public AudioSource printerSound;
    public AudioSource paperSound;

    bool canSeeUI;

    void Start()
    {
        tutorialUI.SetActive(false);
        printerSound = GetComponent<AudioSource>();
        StartCoroutine(StartGame());
    }

    void OnMouseDown()
    {
        tutorialUI.SetActive(true);
        paperSound.Play();
        model.SetActive(false);
        if(canSeeUI)
        {
            if (Input.GetMouseButtonDown(0))
            {
                tutorialUI.SetActive(false);
            }
        }
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);

        PrintAnimation();
        canSeeUI = true;
    }

    public void PrintAnimation()
    {
        paper.SetTrigger("canPrint");
    }

    public void PlayPrintSound()
    {
        printerSound.Play();
    }
}
