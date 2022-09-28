using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    private CarMovement carMovement;

    private void Awake()
    {
        carMovement = GetComponent<CarMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = Vector3.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        carMovement.SetInputVector(inputVector);
        // add comment for git test
    }
}
