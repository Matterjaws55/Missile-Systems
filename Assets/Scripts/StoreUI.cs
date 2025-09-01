using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public GameObject storeUI;
    public GameObject storeUIPanel;

    public GameObject[] items;

    public AudioSource purchaseSound;
    public AudioSource enterStore;
    public AudioSource exitStore;

    void Start()
    {
        storeUI.SetActive(false);
        storeUIPanel.SetActive(false);
    } 

    void OnMouseDown()
    {
        storeUI.SetActive(true);
        storeUIPanel.SetActive(true);
        enterStore.Play();
    }

    public void ItemSelected(Button button)
    {
        button.gameObject.SetActive(false);
        purchaseSound.Play();
    }

    public void Picture1()
    {
        if (items.Length > 0) items[0]?.SetActive(true);
    }
    public void Picture2()
    {
        if (items.Length > 1) items[1]?.SetActive(true);
    }
    public void Picture3()
    {
        if (items.Length > 2) items[2]?.SetActive(true);
    }
    public void Magic8Ball()
    {
        if (items.Length > 3) items[3]?.SetActive(true);
    }


    public void CloseStore()
    {
        storeUI.SetActive(false);
        storeUIPanel.SetActive(false);
        exitStore.Play();
    }
}
