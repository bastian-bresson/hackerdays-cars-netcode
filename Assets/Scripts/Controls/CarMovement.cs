using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CarMovement : NetworkBehaviour
{
    [SerializeField] private Transform thirdPersonAnchor;
    
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30f;
    public float turnFactor = 3.5f;

    private NetworkVariable<float> accelerationInput = new NetworkVariable<float>();
    private NetworkVariable<float> steeringInput = new NetworkVariable<float>();

    private float rotationAngle = 90;

    private Rigidbody carRigidBody;
    private float velocityForward;
    private float maximumSpeed = 20;
    private float minimumTurnSpeedFactor = 8;
    private SpawnPoint spawnPoint;

    private void Awake()
    {
    }
    
    void Update()
    {
    }
    private void UpdateServer()
    {
        ApplyEngineForce();
        ApplySteering();
    }

    private void UpdateClient()
    {
        
    }

    public override void OnNetworkSpawn()
    {
        carRigidBody = GetComponentInChildren<Rigidbody>();
        if (carRigidBody == null)
            Debug.Log($"There's no rigid body here, matey! :(");
        
        base.OnNetworkSpawn();
        if (IsServer)
        {
            SetStartPosition();
        }
        
        if (IsOwner)
        {
            CameraManager.instance.ClaimThirdPersonCamera(thirdPersonAnchor);
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if (IsServer)
        {
            ReleaseSpawnPoint();
        }
    }

    private void ReleaseSpawnPoint()
    {
        SpawnManager.instance.releaseSpawnPoint(spawnPoint);
    }

    private void SetStartPosition()
    {
        spawnPoint = SpawnManager.instance.GetNextAvailableSpawnPoint();
        transform.parent.position = spawnPoint.getPosition();
        transform.rotation = Quaternion.Euler(new Vector3(0,rotationAngle,0));
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
        if (accelerationInput.Value == 0)
        {
            carRigidBody.drag = 1.5f;
        }
        else
        {
            carRigidBody.drag = 0;
        }
        
        velocityForward = Vector3.Dot(carRigidBody.transform.forward, carRigidBody.velocity);

        if (velocityForward > maximumSpeed && accelerationInput.Value > 0) return;

        if (velocityForward < -maximumSpeed / 1.5f && accelerationInput.Value < 0) return;
        // Vector3 engineForceVector = transform.forward * accelerationInput * accelerationFactor;
        Vector3 engineForceVector = carRigidBody.transform.forward * accelerationInput.Value * accelerationFactor;

        carRigidBody.AddForce(engineForceVector, ForceMode.Force);
    }

    private void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = carRigidBody.velocity.magnitude / minimumTurnSpeedFactor;
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        if (velocityForward > 0)
        {
            rotationAngle += steeringInput.Value * turnFactor * minSpeedBeforeAllowTurningFactor;
        } else
        {
            rotationAngle -= steeringInput.Value * turnFactor * minSpeedBeforeAllowTurningFactor;
        }
        carRigidBody.MoveRotation(Quaternion.Euler(0, rotationAngle, 0));
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
