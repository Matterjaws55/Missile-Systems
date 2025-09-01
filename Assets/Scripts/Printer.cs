using UnityEngine;
using System.Collections;
using UnityEngine.ProBuilder;
using UnityEngine.XR;
using Unity.VisualScripting.Antlr3.Runtime;

public class Printer : MonoBehaviour
{
    public GameObject tutorialUI;
    public GameObject model;
    public Animator paper;
    public AudioSource printerSound;
    public AudioSource paperSound;

    bool isOpen = false;

    void Start()
    {
        tutorialUI.SetActive(false);
        printerSound = GetComponent<AudioSource>();
        StartCoroutine(StartGame());
    }

    void OnMouseDown()
    {
        paperSound.Play();
        model.SetActive(false);
        if (!isOpen)
        {
            tutorialUI.SetActive(true);
            isOpen = true;
        }
    }

    void Update()
    {
        if (isOpen && Input.GetMouseButtonDown(0))
        {
            tutorialUI.SetActive(false);
            isOpen = false;
        }
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);

        PrintAnimation();
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
