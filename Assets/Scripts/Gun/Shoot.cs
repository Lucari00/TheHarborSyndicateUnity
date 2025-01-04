using UnityEngine;

public class Shoot : MonoBehaviour {

    [SerializeField] private GameObject bulletObject;
    [SerializeField] private GameObject canon;
    [SerializeField] private GameObject fxSmoke;
    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform characterOrientation;
    [SerializeField] private float shootCooldown;
    [SerializeField] private bool readyToShoot;
    public bool hasGun;
    [SerializeField] private bool isPlayer;
    public bool isPaused;

    public Transform orientation;

    private void Start() {
        isPaused = false;
        readyToShoot = true;
        if (!isPlayer) {
            hasGun = true;
        }
    }

    private void getInput() {
        if (Input.GetKey(shootKey) && readyToShoot && hasGun && !isPaused) {
            readyToShoot = false;
            Fire();
            Invoke("ResetShoot", shootCooldown);
        }
    }

    private void ResetShoot() {
        readyToShoot = true;
    }

    public void Fire() {
        Quaternion rotation = Quaternion.Euler(-8f, characterOrientation.localRotation.eulerAngles.y -0.2f, characterOrientation.localRotation.eulerAngles.z + 90);
        if(!isPlayer) {
            rotation = Quaternion.Euler(-8f, characterOrientation.localRotation.eulerAngles.y + 90f, characterOrientation.localRotation.eulerAngles.z + 90);
        }
        
        GameObject bulletObj = Instantiate(bulletObject, canon.transform.position, rotation);
        bulletObj.GetComponent<Bullet>().isPlayerBullet = isPlayer;
        Instantiate(fxSmoke, canon.transform.position + new Vector3(0f, 0.05f, 0f), rotation);
        PlaySound();
    }

    // Update is called once per frame
    void Update() {
        if (isPlayer) {
            getInput();
        }
    }

    void PlaySound() {
        if (audioSource != null) {
            audioSource.Play();
        }
    }
}