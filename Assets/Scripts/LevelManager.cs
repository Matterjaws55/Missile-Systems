using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Level Setup")]
    public Level[] levels;
    private int currentLevelIndex = 0;
    private bool canChangeLevel = false;

    private List<GameObject> activeShips = new List<GameObject>();

    void Start()
    {
        StartLevel(currentLevelIndex);
    }

    void StartLevel(int index)
    {
        if (index >= levels.Length)
        {
            return;
        }

        Debug.Log("Starting level: " + levels[index].levelName);
        canChangeLevel = false;

        foreach (var spawn in levels[index].ships)
        {
            if (spawn.shipPrefab != null)
            {
                Quaternion rot = Quaternion.Euler(spawn.rotation);
                GameObject newShip = Instantiate(spawn.shipPrefab, spawn.position, rot);
                activeShips.Add(newShip);

                // Track ship destruction
                ShipManager ship = newShip.AddComponent<ShipManager>();
                ship.onDestroyed += OnShipDestroyed;
            }
        }
    }

    void OnShipDestroyed(GameObject ship)
    {
        activeShips.Remove(ship);

        if (activeShips.Count == 0)
        {
            canChangeLevel = true;
        }
    }

    public void NextLevel()
    {
        if (canChangeLevel)
        {
            currentLevelIndex++;
            StartLevel(currentLevelIndex);
        }
    }
}