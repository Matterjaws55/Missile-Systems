using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public GameObject ships;
    public LevelManager spawner;
    public AudioSource shipStart;
    public GameObject missileScreen;
    public GameObject storeScreen;

    private bool hasPlayed = false;

    void Start()
    {
        ships.SetActive(false);
    }

    public void GameStart()
    {        
        Debug.Log("Ships Spawned");
        spawner.NextLevel();
        ships.SetActive(true);
        missileScreen.SetActive(true);
        storeScreen.SetActive(false);

        if (!hasPlayed)
        {
            shipStart.Play();
            hasPlayed = true;
        }
    }
}
