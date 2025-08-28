using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwitchCam : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject mainCamera;              
    public GameObject screenCamera;            

    [Header("UI")]
    public GameObject screenUI;   
    public Button returnButton;

    [Header("Post Processing")]
    public GameObject missileVision;
    public GameObject directionalLighting;

    [Header("Audio")]
    public AudioSource enterMissileView;
    public AudioSource exitMissileView;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMask = ~LayerMask.GetMask("UI"); 
            if (Physics.Raycast(ray, out hit, 1000f, layerMask))
            {
                if (hit.transform == transform)
                {
                    enterMissileView.Play();

                    mainCamera.SetActive(false);
                    screenCamera.SetActive(true);

                    screenUI.SetActive(true);
                    missileVision.SetActive(true);
                    directionalLighting.SetActive(true);

                    Cursor.visible = false;
                }
            }
        }
    }

    public void SwitchBackCamera()
    {
        exitMissileView.Play();

        mainCamera.SetActive(true);
        screenCamera.SetActive(false);

        screenUI.SetActive(false);
        missileVision.SetActive(false);
        directionalLighting.SetActive(false);

        Cursor.visible = true;
    }
}