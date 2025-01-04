using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DetectionShoot : AbstractTutoClass
{
    public GameObject textKey;
    public GameObject playerObject;
    public GameObject characterObject;
    public GameObject crosshairObject;
    public KeyCode keyGet = KeyCode.E;
    private bool isInRange;
    private Shoot shootScript;

    private void Start() {
        textKey.SetActive(false);
        shootScript = playerObject.GetComponent<Shoot>();
    }

    private void FixedUpdate() {
        GetInput();
    }

    private void GetInput() {
        if (Input.GetKey(keyGet) && isInRange) {
            TakeGun();
        }
    }

    private void TakeGun() {
        shootScript.hasGun = true;
        propsObjects[0].SetActive(false);
        textKey.SetActive(false);
        characterObject.SetActive(true);
        crosshairObject.SetActive(true);
        gameObject.SetActive(false);
        isFinish = true;
    }

    void OnTriggerEnter(Collider other) {
        // Vérifie si le joueur est entré dans la zone de détection de la porte
        if (other.CompareTag("Player")) {
            textKey.GetComponent<TextMeshProUGUI>().text = "Press E to get the gun";
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
