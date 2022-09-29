using System;
using Unity.Netcode;
using UnityEngine;

public class Checkpoint : NetworkBehaviour
{
    [SerializeField] private bool isStart;
    [SerializeField] private int checkPointOrderOnTrack;
    [SerializeField] private int previousCheckPointOrderOnTrack;
    [SerializeField] private BoxCollider boxCollider;
    
    
    private void Awake()
    {
        boxCollider.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            boxCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");
        var playerRacePosition = other.gameObject.GetComponent<PlayerRacePosition>();

        if (playerRacePosition == null) return;
        
        Debug.Log("Found Player Position");

        if (playerRacePosition.checkpointNumber.Value != previousCheckPointOrderOnTrack)
        {
            Debug.Log("Wrong checkpoint!");
            return;
        }

        if (isStart)
        {
            playerRacePosition.lapNumber.Value++;
        }

        playerRacePosition.checkpointNumber.Value = checkPointOrderOnTrack;
        Debug.Log("Lap: " + playerRacePosition.lapNumber.Value + " Checkpoint:" + playerRacePosition.checkpointNumber.Value);
    }
}