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

    private float accelerationInput = 0;
    private float steeringInput = 0;

    private float rotationAngle = 0;

    private Rigidbody carRigidBody;


    private void Awake()
    {
        carRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyEngineForce();
        
        //KillOrthogonalVelocity();
        
        ApplySteering();
    }

    private void ApplyEngineForce()
    {
        Vector3 engineForceVector = transform.forward * accelerationInput * accelerationFactor;

        carRigidBody.AddForce(engineForceVector, ForceMode.Force);
    }

    private void ApplySteering()
    {
        rotationAngle -= steeringInput * turnFactor;
        carRigidBody.MoveRotation(Quaternion.Euler(0,rotationAngle,0));
        
    }

    
    //HELP. How to vector math when not 2D?!?
    private void KillOrthogonalVelocity()
    {
        Vector3 forwardVelocity = transform.forward * Vector3.Dot(carRigidBody.velocity, transform.forward);
        Vector3 rightVelocity = transform.right * Vector3.Dot(carRigidBody.velocity, transform.right);

        //carRigidBody.velocity = forwardVelocity * rightVelocity * driftFactor;
    }
    
    public void SetInputVector(Vector3 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
    
}
