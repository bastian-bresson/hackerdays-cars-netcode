using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerCountHolder : NetworkBehaviour
{
    public static NetworkVariable<int> playerCount;

}
