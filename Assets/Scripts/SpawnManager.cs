using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System.Linq;

public class SpawnManager : NetworkBehaviour
{
    [SerializeField]
    public List<SpawnPoint> spawnPoints;

    public static SpawnManager instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SpawnPoint GetNextAvailableSpawnPoint()
    {
        var availaleSpawnPoint = spawnPoints.First(spawnPoint => !spawnPoint.isUsed);
        return availaleSpawnPoint;
    }
}
