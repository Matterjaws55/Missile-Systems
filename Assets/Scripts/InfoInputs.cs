using UnityEngine;

public class InfoInputs : MonoBehaviour
{
    public GameObject meInfo;
    public GameObject projectInfo;

    void start()
    {
        meInfo.SetActive(false);
        projectInfo.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (meInfo.activeSelf)
            {
                meInfo.SetActive(false);
            }
            else
            {
                meInfo.SetActive(true);
                projectInfo.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (projectInfo.activeSelf)
            {
                projectInfo.SetActive(false);
            }
            else
            {
                projectInfo.SetActive(true);
                meInfo.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            meInfo.SetActive(false);
            projectInfo.SetActive(false);
        }
    }
}