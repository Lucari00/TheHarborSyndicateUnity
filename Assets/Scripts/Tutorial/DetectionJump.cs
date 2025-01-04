using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionJump : AbstractTutoClass
{
    private void Start() {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isFinish = true;
            gameObject.SetActive(false);
        }
    }
}
