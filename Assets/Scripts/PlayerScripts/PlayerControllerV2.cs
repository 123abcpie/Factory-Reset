using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle;
    

    // Settings
    [SerializeField] private float motorForce, maxSteerAngle;

    // Tread Colliders
    [SerializeField] private WheelCollider leftTreadCollider, rightTreadCollider;
    

    // Treads
    [SerializeField] private Transform leftTreadTransform, rightTreadTransform;

    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateTreads();
        //isRolling();
    }

    private void GetInput() {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Roll Input
        //isRolling = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor() {
        leftTreadCollider.motorTorque = verticalInput * motorForce;
        rightTreadCollider.motorTorque = verticalInput * motorForce;
        
    }


    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        leftTreadCollider.steerAngle = currentSteerAngle;
        rightTreadCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateTreads() {
        UpdateSingleTread(leftTreadCollider, leftTreadTransform);
        UpdateSingleTread(rightTreadCollider, rightTreadTransform);
       
    }

    private void UpdateSingleTread(WheelCollider treadCollider, Transform treadTransform) {
        Vector3 pos;
        Quaternion rot; 
        treadCollider.GetWorldPose(out pos, out rot);
        treadTransform.rotation = rot;
        treadTransform.position = pos;
    }

}
