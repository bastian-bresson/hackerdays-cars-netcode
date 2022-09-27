using System;
using Unity.Netcode;
using UnityEngine;

namespace Hello_World
{
    public class NetworkTransformTest : NetworkBehaviour
    {
        private void Update()
        {
            if (!IsServer) return;

            var theta = Time.frameCount / 100.0f;
        
            transform.position = new Vector3((float) Math.Cos(theta), 0.0f, (float) Math.Sin(theta));
        }
    }
}