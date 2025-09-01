using UnityEngine;

public class SwitchScreenToStore : MonoBehaviour
{
    public GameObject missileScreen;
    public GameObject storeScreen;

    private bool showingStore = false;

    private void Start()
    {
        storeScreen.SetActive(false);
        missileScreen.SetActive(true);
    }

    public void SwitchScreen()
    {
        if (!showingStore)
        {
            missileScreen.SetActive(false);
            storeScreen.SetActive(true);
            showingStore = true;
        }
        else
        {
            missileScreen.SetActive(true);
            storeScreen.SetActive(false);
            showingStore = false;
        }
    }
}
