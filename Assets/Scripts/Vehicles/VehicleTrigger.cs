using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VehicleTrigger : MonoBehaviour
{
    private GameObject player;

    private PlayerMovement playerMovement;
    private WheeledVehicleController vehicleController;

    private GameObject textKey;

    bool inRange;

    bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        inRange = false;
        player = GameObject.Find("PlayerController").transform.GetChild(0).gameObject;
        playerMovement = player.GetComponent<PlayerMovement>();
        vehicleController = transform.parent.GetComponent<WheeledVehicleController>();
        textKey = GameObject.Find("UI").transform.Find("KeyToShow").gameObject;
    }

    private void Update() {
        if (inRange && active) {
            if (Input.GetKeyDown(KeyCode.E)) {
                EnterVehicle();
            }
        }
        if (vehicleController.player != null) {
            if (Input.GetKeyDown(KeyCode.E)) {
                ExitVehicle();
            }
        }
    }

    private void EnterVehicle() {
        if (vehicleController is TruckController) {
            ((TruckController)vehicleController).truckDoors.DeactivateTrigger();
        }
        inRange = false;
        active = false;
        textKey.SetActive(false);
        playerMovement.GetIntoVehicle(vehicleController);
    }

    private void ExitVehicle() {
        if(vehicleController is TruckController) {
            ((TruckController)vehicleController).truckDoors.ActivateTrigger();
        }
        StartCoroutine(ActivateTrigger());
        playerMovement.GetOutOfVehicle(vehicleController, vehicleController.exitPosition);
    }

    private IEnumerator ActivateTrigger() {
        yield return new WaitForSeconds(1);
        active = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && active) {
            inRange = true;
            textKey.GetComponent<TextMeshProUGUI>().text = "Press E to enter";
            textKey.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player") && active) {
            inRange = false;
            textKey.SetActive(false);
        }
    }
}
