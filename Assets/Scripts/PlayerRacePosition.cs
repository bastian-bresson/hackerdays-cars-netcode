using Unity.Netcode;
using UnityEngine;

public class PlayerRacePosition : NetworkBehaviour
{
    public NetworkVariable<int> lapNumber = new NetworkVariable<int>(0);
    public NetworkVariable<int> checkpointNumber = new NetworkVariable<int>(0);

    public override void OnNetworkSpawn()
    {
        if (!IsClient) return;
        
        lapNumber.OnValueChanged += OnLabChanged;
        checkpointNumber.OnValueChanged += OnCheckpointChanged;

        Setup();
    }

    private void OnCheckpointChanged(int previousvalue, int newvalue)
    {
        Debug.Log($"Checkpoint {newvalue} reached");
        UpdateRacePosition(lapNumber.Value, newvalue);
    }

    private void OnLabChanged(int previousvalue, int newvalue)
    {
        UpdateRacePosition(newvalue, checkpointNumber.Value);
    }

    private void Setup()
    {
        Leaderboard.Instance.RegisterPlayer(OwnerClientId.ToString());
    }

    private void UpdateRacePosition(int lap, int checkpoint)
    {
        Leaderboard.Instance.UpdatePlayerPlacement("Player " + OwnerClientId, lap, checkpoint);
    }
}