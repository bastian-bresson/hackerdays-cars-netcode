using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CarMovement : NetworkBehaviour
{
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30f;
    public float turnFactor = 3.5f;

    private NetworkVariable<float> accelerationInput = new NetworkVariable<float>();
    private NetworkVariable<float> steeringInput = new NetworkVariable<float>();

        private float rotationAngle = 0;

    private Rigidbody carRigidBody;


    private void Awake()
    {
        carRigidBody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
    }
    private void UpdateServer()
    {
        ApplyEngineForce();
    }

    private void UpdateClient()
    {
        // Set forward
        
        // ApplySteering();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsServer)
        {
            UpdateServer();
        }

        if (IsClient)
        {
            UpdateClient();
        }
    }
    
    private void ApplyEngineForce()
    {
        // Vector3 engineForceVector = transform.forward * accelerationInput * accelerationFactor;
        Vector3 engineForceVector = transform.forward * accelerationInput.Value * accelerationFactor;

        carRigidBody.AddForce(engineForceVector, ForceMode.Force);
    }
    
    private void ApplySteering()
    {
        rotationAngle -= steeringInput.Value * turnFactor;
        carRigidBody.MoveRotation(Quaternion.Euler(0,rotationAngle,0));
    }

    private void UpdateClientEngineForce()
    {
     
        
    }

    private void UpdateClientSteering()
    {
        
        
    }

        //HELP. How to vector math when not 2D?!?
        private void KillOrthogonalVelocity()
        {
            Vector3 forwardVelocity = transform.forward * Vector3.Dot(carRigidBody.velocity, transform.forward);
            Vector3 rightVelocity = transform.right * Vector3.Dot(carRigidBody.velocity, transform.right);

            //carRigidBody.velocity = forwardVelocity * rightVelocity * driftFactor;
        }
    
    [ServerRpc]
    public void SetInputVectorServerRpc(Vector3 inputVector)
    {
        steeringInput.Value = inputVector.x;
        accelerationInput.Value = inputVector.y;
    }
    
}
