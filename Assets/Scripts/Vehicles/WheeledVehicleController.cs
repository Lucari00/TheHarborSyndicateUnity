using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class WheeledVehicleController : VehicleController {
    protected float currentSteerAngle, currentbrakeForce;
    protected bool isBraking;
    protected int numberOfWheels;

    // Wheel Colliders
    protected WheelCollider[] wheelColliders;

    // Wheels
    protected Transform[] wheelTransforms;

    [Header("Wheeled Vehicle Settings")]
    [SerializeField] protected float motorForce;
    [SerializeField] protected float brakeForce, maxSteerAngle;
    [SerializeField] protected int numberOfDriveWheels = 2;

    [Header("Wheeled Vehicle Components")]
    [SerializeField] protected GameObject steeringWheel;
    [SerializeField] protected GameObject meshesParent;
    [SerializeField] protected GameObject collidersParent;

    [Header("Wheeled Vehicle Sound Settings")]
    [SerializeField] private float minPitch = 0.5f;
    [SerializeField] private float maxPitch = 4.0f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private AudioSource vehicleSound;

    protected override void Start() {
        base.Start();
        if (collidersParent.transform.childCount != meshesParent.transform.childCount) {
            Debug.LogError("Number of wheel colliders and meshes do not match");
            return;
        }
        numberOfWheels = collidersParent.transform.childCount;
        wheelColliders = new WheelCollider[numberOfWheels];
        wheelTransforms = new Transform[numberOfWheels];
        for(int i = 0; i < numberOfWheels; i++) {
            wheelColliders[i] = collidersParent.transform.GetChild(i).GetComponent<WheelCollider>();
            wheelTransforms[i] = meshesParent.transform.GetChild(i).GetComponent<Transform>();
            //Debug.Log("Wheel " + i + " initialized " + collidersParent.transform.GetChild(i).name + " " + meshesParent.transform.GetChild(i).name);
        }
    }

    protected override void FixedUpdate() {
        ManageSound();
        if (player == null) {
            return;
        }
        base.FixedUpdate();
        Move();
    }

    protected override void Move() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    protected override void GetInput() {
        base.GetInput();
        isBraking = Input.GetKey(KeyCode.Space);
    }

    protected virtual void HandleMotor() {
        for(int i = 0; i < numberOfDriveWheels; i++) {
            wheelColliders[i].motorTorque = verticalInput * motorForce;
        }
        if(isBraking) {
            currentbrakeForce = brakeForce;
        }
        else {
            currentbrakeForce = 0;
        }
        ApplyBraking();
    }

    protected virtual void ApplyBraking() {
        foreach(WheelCollider collider in wheelColliders) {
            collider.brakeTorque = currentbrakeForce;
        }
    }

    protected virtual void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        if (numberOfWheels > 2) {
            WheelCollider frontLeftWheelCollider = wheelColliders[0];
            WheelCollider frontRightWheelCollider = wheelColliders[1];
            frontLeftWheelCollider.steerAngle = currentSteerAngle;
            frontRightWheelCollider.steerAngle = currentSteerAngle;
        }
        steeringWheel.transform.localEulerAngles = new Vector3(steeringWheel.transform.localEulerAngles.x, steeringWheel.transform.localEulerAngles.y, -currentSteerAngle*2);
    }

    protected virtual void UpdateWheels() {
        for(int i = 0; i < wheelColliders.Length; i++) {
            UpdateSingleWheel(wheelColliders[i], wheelTransforms[i]);
        }
    }

    protected virtual void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelTransform.SetPositionAndRotation(pos, rot);
    }

    protected override void ManageSound() {
        if (player != null) {
            if (vehicleSound != null && !vehicleSound.isPlaying) {
                vehicleSound.Play();
            }
            float speed = Mathf.Abs(vehicleRb.velocity.magnitude);
            vehicleSound.pitch = Mathf.Lerp(minPitch, maxPitch, (Mathf.Abs(verticalInput) * maxSpeed / 8 + speed) / maxSpeed);
        } else {
            if (vehicleSound != null) {
                vehicleSound.Stop();
            }
        }
    }
}