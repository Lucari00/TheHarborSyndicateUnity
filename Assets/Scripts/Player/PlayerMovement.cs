using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed;
    public float maxSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode shiftKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundMask;
    bool isGrounded;

    public Transform orientation;

    public Camera mainCamera;

    private CameraTransition cameraTransition;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private Shoot shootScript;
    private GameObject player;

    private GameObject crosshair;
    [SerializeField] private TextMeshProUGUI keyText;

    [SerializeField] private GameObject transitionScreen;

    private bool hasGun;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        cameraTransition = transitionScreen.GetComponent<CameraTransition>();
        shootScript = GetComponent<Shoot>();
        crosshair = GameObject.Find("Crosshair");
    }

    public void GetIntoVehicle(VehicleController vehicleC) {
        if (crosshair == null) {
            crosshair = GameObject.Find("Crosshair");
        }
        cameraTransition.ChangeCamera(mainCamera, vehicleC.vehicleCamera);
        transform.gameObject.SetActive(false);
        hasGun = shootScript.hasGun;
        shootScript.hasGun = false;
        bool active = transform.gameObject.activeSelf;
        if(!active) {
            transform.gameObject.SetActive(true);
        }
        StartCoroutine(ManagePlayerInVehicleCoroutine(vehicleC, active));
        if (crosshair != null) {
            crosshair.SetActive(false);
        }
        keyText.text = "Press E to exit or R to reset the position of the vehicle.";
        keyText.gameObject.SetActive(true);
    }

    public void GetOutOfVehicle(VehicleController vehicleC, GameObject exitPosition) {
        cameraTransition.ChangeCamera(vehicleC.vehicleCamera, mainCamera);
        transform.position = exitPosition.transform.position;
        transform.gameObject.SetActive(true);
        shootScript.hasGun = hasGun;
        StartCoroutine(ManagePlayerInVehicleCoroutine(vehicleC));
        if (crosshair != null) {
            crosshair.SetActive(true);
        }
        keyText.gameObject.SetActive(false);
    }

    private IEnumerator ManagePlayerInVehicleCoroutine(VehicleController vehicleC, bool active=true) {
        yield return new WaitForSeconds(1f);

        if(vehicleC.player == null) {
            vehicleC.player = gameObject;
        }
        else {
            vehicleC.player = null;
        }
        if(!active) {
            transform.gameObject.SetActive(false);
        }
    }

    public float GetPlayerSpeed() {
        if(isGrounded) {
            return rb.velocity.magnitude;
        }
        else {
            return -1.0f;
        }
    }

    private void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(jumpKey) && isGrounded && readyToJump) {
            readyToJump = false;
            maxSpeed = moveSpeed;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        } else if (Input.GetKey(shiftKey) && isGrounded) {
            maxSpeed = moveSpeed + 3; 
        } else {
            maxSpeed = moveSpeed;
        }
    }

    private void MovePlayer() {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (isGrounded) {
            rb.AddForce(moveDirection.normalized * maxSpeed * 10f, ForceMode.Force);
        } else if (!isGrounded) {
            rb.AddForce(moveDirection.normalized * maxSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    void FixedUpdate() {
        if (!transform.parent.gameObject.activeSelf) {
            return;
        }
        MovePlayer();
    }

    private void Update() {
        if(!transform.parent.gameObject.activeSelf) {
            return;
        }
        //Ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.5f, groundMask);

        GetInput();
        SpeedControl();

        // Ground drag

        if (isGrounded) {
            rb.drag = groundDrag;
        } else {
            rb.drag = 0;
        }
    }

    private void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (flatVel.magnitude > maxSpeed) {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump() {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        readyToJump = true;
    }
}
