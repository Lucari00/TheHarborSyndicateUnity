using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectionCloseDoorsTruck : AbstractTutoClass
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isFinish = true;
            gameObject.SetActive(false);
        }
    }
}
