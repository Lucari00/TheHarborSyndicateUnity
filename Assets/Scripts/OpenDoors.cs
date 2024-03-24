using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public Animator rightDoor = null;
    //public Animator leftDoor = null;

    void Start() {
    }

    void Update() {
    }

    void OnTriggerEnter(Collider other) {
        // V�rifie si le joueur est entr� dans la zone de d�tection de la porte
        if (other.CompareTag("PlayerC")) {
            Debug.Log("open");
            rightDoor.Play("DoorOpenRight", 0, 0.0f);
            rightDoor.Play("DoorOpenLeft", 0, 0.0f);
            gameObject.SetActive(false);
        }
    }

    /*void OnTriggerExit(Collider other) {
        // V�rifie si le joueur est sorti de la zone de d�tection de la porte
        if (other.CompareTag("Player")) {
            playerInRange = false;
        }
    }*/
}