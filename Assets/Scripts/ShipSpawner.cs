using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    public GameObject ships;
    public LevelManager spawner;
    public AudioSource shipStart;

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
        
        if(!hasPlayed)
        {
            shipStart.Play();
            hasPlayed = true;
        }
    }
}
