using System;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public Action<GameObject> onDestroyed;

    void OnDestroy()
    {
        if (onDestroyed != null)
            onDestroyed(gameObject);
    }
}
