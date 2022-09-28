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

    private void OnCollisionEnter(Collision collision)
    {
        var playerRacePosition = collision.gameObject.GetComponent<PlayerRacePosition>();

        if (playerRacePosition == null) return;

        if (isStart)
        {
            playerRacePosition.labNumber.Value++;
        }

        playerRacePosition.checkpointNumber.Value = checkPointOrderOnTrack;
    }
}