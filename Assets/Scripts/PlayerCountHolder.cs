using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerCountHolder : NetworkBehaviour
{
    public static PlayerCountHolder instance;
    
    public NetworkVariable<int> playerCount;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
}
