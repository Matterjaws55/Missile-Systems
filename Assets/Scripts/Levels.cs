using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Scriptable Objects/Levels")]

public class Level : ScriptableObject
{
    public string levelName;
    public ShipSpawn[] ships;
}

[System.Serializable]
public class ShipSpawn
{
    public GameObject shipPrefab;  
    public Vector3 position;      
    public Vector3 rotation;  
}