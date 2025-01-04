using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{

    private float sensX;
    private float sensY;

    public Transform orientation;
    public Transform characterOrientation;

    public GameObject pauseMenu;
    public bool isPaused;

    // à l'arrache, à changer
    public Slider sensibilityXSlider;
    public Slider sensibilityYSlider;

    private float xRotation;
    private float yRotation;

    private Shoot shootScript;
    private GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sensX = 100f;
        sensY = 100f;
        GameObject player = GameObject.Find("Player");
        shootScript = player.GetComponent<Shoot>();
        crosshair = GameObject.Find("Crosshair");
    }

    public void OnSensibilityXChanged(System.Single changedSensX) {
        sensX = sensibilityXSlider.value;
    }

    public void OnSensibilityYChanged(System.Single changedSensY) {
        sensY = sensibilityYSlider.value;
    }

    void GetInputs() {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            isPaused = true;
            shootScript.isPaused = true;
            if (crosshair != null) {
                crosshair.SetActive(false);
            }
        } else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) {
           QuitMenu();
        }
    }

    public void OnPauseMenuQuit() {
        QuitMenu();
    }

    private void QuitMenu() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        isPaused = false;
        shootScript.isPaused = false;
        if (crosshair != null) {
            crosshair.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();

        if (isPaused) {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
        characterOrientation.rotation = Quaternion.Euler(0f, yRotation + 90f, xRotation);
    }
}
