using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    public static CameraManager instance;

    // Start is called before the first frame update
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ClaimThirdPersonCamera(Transform thirdPersonAnchor)
    {
        cinemachineVirtualCamera.LookAt = thirdPersonAnchor;
        cinemachineVirtualCamera.Follow = thirdPersonAnchor;
    }
}
