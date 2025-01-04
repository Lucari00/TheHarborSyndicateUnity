using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TruckDoors : MonoBehaviour
{
    [SerializeField] GameObject rightDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] AudioSource closeDoors;
    [SerializeField] AudioSource openDoors;

    [HideInInspector] public GameObject textKey;

    private Animator rightAnimator;
    private Animator leftAnimator;
    public bool open;
    private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        rightAnimator = rightDoor.GetComponent<Animator>();
        leftAnimator = leftDoor.GetComponent<Animator>();
        isMoving = false;

        textKey = GameObject.Find("UI").transform.Find("KeyToShow").gameObject;
    }

    public void DeactivateTrigger() {
        gameObject.SetActive(false);
    }

    public void ActivateTrigger() {
        gameObject.SetActive(true);
    }

    private void ResetIsMoving() {
        isMoving = false;
    }

    public void OpenRightDoor() {
        rightAnimator.Play("OpenRight", 0, 0.0f);
    }

    public void OpenLeftDoor() {
        leftAnimator.Play("OpenLeft", 0, 0.0f);
    }

    public void CloseRightDoor() {
        rightAnimator.Play("CloseRight", 0, 0.0f);
    }

    public void CloseLeftDoor() {
        leftAnimator.Play("CloseLeft", 0, 0.0f);
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetKey(KeyCode.O) && !open && !isMoving) {
                OpenRightDoor();
                OpenLeftDoor();
                openDoors.Play();   
                open = true;
                isMoving = true;
                Invoke("ResetIsMoving", 1f);
            }
            if (Input.GetKey(KeyCode.C) && open && !isMoving) {
                CloseRightDoor();
                CloseLeftDoor();
                Invoke("PlayCloseDoors", 0.7f);
                open = false;
                isMoving = true;
                Invoke("ResetIsMoving", 1f);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            textKey.GetComponent<TextMeshProUGUI>().text = "Press C to close or O to open";
            textKey.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            textKey.SetActive(false);
        }
    }

    void PlayCloseDoors() {
        closeDoors.Play();
    }
}