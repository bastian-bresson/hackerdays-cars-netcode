using System;
using Unity.Netcode;
using UnityEngine;

public class Checkpoint : NetworkBehaviour
{
    [SerializeField] private bool isStart;
    [SerializeField] private int checkPointOrderOnTrack;
    
    
    private void Awake()
    {
        enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");
        var playerRacePosition = other.gameObject.GetComponent<PlayerRacePosition>();

        if (playerRacePosition == null) return;
        
        Debug.Log("Found Player Position");

        if (isStart)
        {
            playerRacePosition.labNumber.Value++;
        }

        playerRacePosition.checkpointNumber.Value = checkPointOrderOnTrack;
    }
}