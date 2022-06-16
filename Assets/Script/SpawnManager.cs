using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public Transform[] spawnPoints;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
        Instance = this;
    }

    public Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
    }

}
