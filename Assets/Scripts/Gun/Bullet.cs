using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public GameObject fxSmoke;
    public GameObject bloodObject;
    public GameObject trailEffect;
    public bool isMoving = true;
    public bool isPlayerBullet = false;

    private GameObject trailEffectObject;

    private void Start() {
        //Invoke("CreateTrail", 0.1f);
        Invoke("SupprBullet", 30f); // opti suppr bullet after 30s
    }

    private void SupprBullet() {
        Destroy(gameObject);
    }

    private void CreateTrail() {
        //GameObject cube = gameObject.GetComponentInChildren<Transform>().gameObject;
        trailEffectObject = Instantiate(trailEffect, transform.position, Quaternion.Euler(0f, 90f, 0f)); // trouver le bon angle et la pos
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 300 || transform.position.x < -300 || transform.position.z > 300 || transform.position.z < -300) {
            SupprBullet();
        }
        if (isMoving) {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (!isPlayerBullet) {
                Instantiate(bloodObject, transform.position, transform.rotation);
                // hits the capsule collider, not the player
                Player playerScript = other.transform.parent.GetComponent<Player>();
                if (playerScript != null) {
                    playerScript.TakeDamage(10);
                }
            }
            return;
        } else if (other.CompareTag("Enemy")) {
            Instantiate(bloodObject, transform.position, transform.rotation);
            GameObject guardObject = other.transform.parent.parent.gameObject;
            Enemy enemyScript = guardObject.GetComponent<Enemy>();
            if (enemyScript != null) {
                enemyScript.Hit();
            }
        } else {
            Instantiate(fxSmoke, transform.position, transform.rotation);
        }
        isMoving = false;
        SupprBullet();
    }
}
