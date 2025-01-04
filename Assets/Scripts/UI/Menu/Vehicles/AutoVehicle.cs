using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoVehicle : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(10f, 25f);
    }

    // Update is called once per frame
    void Update()
    {
        // le véhicle avance toujours tout droit 
        if (transform.position.z > 100 || transform.position.z < -150) {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }
}
