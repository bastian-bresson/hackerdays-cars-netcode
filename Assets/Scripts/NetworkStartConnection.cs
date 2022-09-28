using Hello_World;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkStartConnection : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabels();
            StartRace();
        }

        GUILayout.EndArea();
    }

    static void StartButtons()
    {
        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    }

    static void StatusLabels()
    {
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

        GUILayout.Label("Transport: " + NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }
    static void StartRace()
    {
        if (!NetworkManager.Singleton.IsServer) return;
        
        if (!GUILayout.Button("Start")) return;
            
        if (!NetworkManager.Singleton.IsClient)
        {
            RaceManager.Instance.StartRace();
        }
    }
}