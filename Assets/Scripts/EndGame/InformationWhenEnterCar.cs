using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InformationWhenEnterCar : MonoBehaviour
{
    private bool inRange = false;
    private GameObject gameMaster;

    private void Start() {
        gameMaster = GameObject.Find("GameMaster");
    }

    private void Update() {
        if (inRange && Input.GetKey(KeyCode.E)) {
            ExecuteEvents.Execute<ICustomMessageTarget>(gameMaster, null, (x, y) => x.VehicleEntered());
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            inRange = true;
        }
    }
}
