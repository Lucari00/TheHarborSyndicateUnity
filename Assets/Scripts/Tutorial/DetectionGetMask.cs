using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectionGetMask : AbstractTutoClass
{

    public GameObject textKey;
    public KeyCode keyGet = KeyCode.E;
    public OpenGarageDoors OpenGarageDoors;
    private bool isInRange;

    private void PutMask() {
        isFinish = true;
        OpenGarageDoors.OpenDoor();
        propsObjects[0].SetActive(false);
        textKey.SetActive(false);
        gameObject.SetActive(false);
    }

    private void GetInputs() {
        if (Input.GetKey(keyGet) && isInRange) {
            PutMask();
        }
    }

    private void FixedUpdate() {
        GetInputs();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            textKey.GetComponent<TextMeshProUGUI>().text = "Press E to put the mask on";
            textKey.SetActive(true);
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            textKey.SetActive(false);
            isInRange = false;
        }
    }
}
