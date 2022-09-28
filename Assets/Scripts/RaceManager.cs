using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class RaceManager : NetworkBehaviour
{
    public static RaceManager Instance;
    public static Action OnRaceStarted;

    [SerializeField] private Canvas startCanvas;

    private bool hasStarted;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void StartRace()
    {
        if (!IsServer || hasStarted) return;

        hasStarted = true;
        
        StartRaceClientRPC();
    }
    
    [ClientRpc]
    void StartRaceClientRPC()
    {
        OnRaceStarted?.Invoke();

        StartCoroutine(ShowStart());
    }

    private IEnumerator ShowStart()
    {
        startCanvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        
        startCanvas.gameObject.SetActive(false);
    }
}