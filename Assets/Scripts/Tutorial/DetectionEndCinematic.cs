using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class DetectionEndCinematic : AbstractTutoClass {

    [SerializeField] private GameObject playerCam;
    [SerializeField] private GameObject cineCam;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCam playerCamScript;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private PlayableDirector cinematic;
    [SerializeField] private OpenGarageDoors openGarageDoors;
    [SerializeField] private CameraTransition cameraTransition;
    [SerializeField] private GameObject tutoVehicle;

    void Start()
    {
        float timeToOpenDoor = openGarageDoors.timeToOpenDoor;
        float endCinematicTime = (float)cinematic.duration;
        this.playerCam.SetActive(false);
        this.playerRb.isKinematic = true;
        this.playerCollider.enabled = false;
        this.playerMovement.enabled = false;
        this.playerCamScript.enabled = false;
        Invoke("OnEndCinematic", endCinematicTime);
        Invoke("OpenDoor", endCinematicTime + timeToOpenDoor);
    }

    void OnEndCinematic() {
        this.missionInformationText = "Enter the warehouse";
        this.playerMovement.enabled = true;
        this.playerCamScript.enabled = true;
        this.playerRb.isKinematic = false;
        this.playerCollider.enabled = true;
        this.tutoVehicle.SetActive(false);
        isFinish = true;
    }

    void OpenDoor() {
        openGarageDoors.OpenDoor();
        gameObject.SetActive(false);
    }
}
