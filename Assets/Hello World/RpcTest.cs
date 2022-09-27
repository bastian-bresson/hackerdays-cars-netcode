using Unity.Netcode;
using UnityEngine;

namespace Hello_World
{
    public class RpcTest : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            if (IsClient)
            {
                TestServerRpc(0);
            }
        }

        [ClientRpc]
        private void TestClientRpc(int value)
        {
            if (!IsClient) return;
        
            Debug.Log("Client Received the RPC #" + value);
            TestServerRpc(value + 1);
        }

        [ServerRpc]
        private void TestServerRpc(int value)
        {
            Debug.Log("Server Received the RPC #" + value);
            TestClientRpc(value);
        }
    }
}