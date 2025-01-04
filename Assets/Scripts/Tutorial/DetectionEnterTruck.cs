using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectionEnterTruck : AbstractTutoClass
{
    public GameObject textKey;
    public KeyCode keyGet = KeyCode.E;
    public GameObject player;
    public TruckController vehicleController;
    public GameObject propToShow;
    public TruckDoors truckDoors;
    public Animator animatorFenceDoor;
    public AudioSource fenceDoorAudio;
    public GameObject crosshair;
    private bool isInRange;
    private PlayerMovement playerMovementScript;
    [SerializeField] private AudioSource radioSound;

    private void Start() {
        isInRange = false;
        playerMovementScript = player.GetComponent<PlayerMovement>();
    }

    private void EnterInTruck() {
        playerMovementScript.GetIntoVehicle(vehicleController);
        propToShow.SetActive(true);
        isFinish = true;
        animatorFenceDoor.Play("OpenFenceDoor");
        fenceDoorAudio.Play();
        radioSound.Play();
        crosshair.SetActive(false);
        textKey.SetActive(false);
        gameObject.SetActive(false);
    }

    private void GetInputs() {
        if (Input.GetKey(keyGet) && isInRange && !truckDoors.open) {
            EnterInTruck();
        } else if (Input.GetKey(keyGet) && isInRange && truckDoors.open) {
            textKey.GetComponent<TextMeshProUGUI>().text = "The truck doors are open. Close them !";
        }
    }

    private void FixedUpdate() {
        GetInputs();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isInRange = true;
            textKey.GetComponent<TextMeshProUGUI>().text = "Press E to get into the truck";
            textKey.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            isInRange = false;
            textKey.SetActive(false);
        }
    }
}
