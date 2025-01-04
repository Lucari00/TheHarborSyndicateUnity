using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VehicleController : MonoBehaviour
{

    protected float horizontalInput, verticalInput;

    protected Rigidbody vehicleRb;

    [HideInInspector] public GameObject player;

    [Header("Global Vehicle Components")]
    public GameObject exitPosition;
    public Camera vehicleCamera;

    private Vector3 startPos;
    private Quaternion startRot;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = null;
        startPos = transform.position;
        startRot = transform.rotation;
        vehicleRb = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate() {
        if(player == null) {
            return;
        }
        if(Input.GetKey(KeyCode.R)) {
            transform.position = startPos;
            transform.rotation = startRot;
        }
    }

    protected virtual void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");

        verticalInput = Input.GetAxis("Vertical");
    }

    protected abstract void Move();

    protected abstract void ManageSound();
}
