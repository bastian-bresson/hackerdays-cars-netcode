using System;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : NetworkBehaviour
{
    [SerializeField] private bool isStart;
    [SerializeField] private int checkPointOrderOnTrack;
    [SerializeField] private int previousCheckPointOrderOnTrack;
    [SerializeField] private Checkpoint nextCheckpoint;
    [SerializeField] private BoxCollider boxCollider;

    [SerializeField] private List<GameObject> nextIndicators;
    [SerializeField] private List<GameObject> notNextIndicators;

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

        if (isStart)
        {
            HighlightClientRpc(true);
        }
    }

    [ClientRpc]
    public void HighlightClientRpc(bool showOnThisClient)
    {
        if (!showOnThisClient) return;
        
        foreach (var nextIndicator in nextIndicators)
        {
            nextIndicator.SetActive(true);
        }
        
        foreach (var nextIndicator in notNextIndicators)
        {
            nextIndicator.SetActive(false);
        }
    }

    [ClientRpc]
    private void StopHighlightingClientRpc(bool showOnThisClient)
    {
        if (!showOnThisClient) return;
        
        foreach (var nextIndicator in nextIndicators)
        {
            nextIndicator.SetActive(false);
        }
        
        foreach (var nextIndicator in notNextIndicators)
        {
            nextIndicator.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLISION");
        var playerRacePosition = other.gameObject.GetComponent<PlayerRacePosition>();

        if (playerRacePosition == null) return;
        
        Debug.Log("Found Player Position");

        if (playerRacePosition.checkpointNumber.Value != previousCheckPointOrderOnTrack && playerRacePosition.lapNumber.Value > 0)
        {
            Debug.Log("Wrong checkpoint!");
            return;
        }
        
        StopHighlightingClientRpc(playerRacePosition.IsLocalPlayer);
        nextCheckpoint.HighlightClientRpc(playerRacePosition.IsLocalPlayer);

        if (isStart)
        {
            playerRacePosition.lapNumber.Value++;
        }

        playerRacePosition.checkpointNumber.Value = checkPointOrderOnTrack;
        Debug.Log("Lap: " + playerRacePosition.lapNumber.Value + " Checkpoint:" + playerRacePosition.checkpointNumber.Value);
    }
}