using System;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    public class CarTrail : MonoBehaviour
    {
        [SerializeField] private List<TrailRenderer> trailRenderers;
        [SerializeField] private Rigidbody carRigidBody;

        [SerializeField] private float requiredVelocity;
        [SerializeField] private float requiredAngularVelocity;

        private bool emit;
        private bool isGrounded;

        private void Update()
        {
            bool shouldEmit = carRigidBody.velocity.magnitude >= requiredVelocity &&
                              carRigidBody.angularVelocity.magnitude >= requiredAngularVelocity &&
                              isGrounded;

            if (emit == shouldEmit) return;
            
            emit = shouldEmit;
            
            foreach (TrailRenderer trailRenderer in trailRenderers)
            {
                trailRenderer.emitting = emit;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            isGrounded = true;
        }

        private void OnTriggerExit(Collider other)
        {
            isGrounded = false;
        }
    }
}