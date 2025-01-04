using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomVehicle : MonoBehaviour
{
    public GameObject[] vehicles;
    public Transform spawnPointTop;
    public Transform spawnPointBottom;
    public float minTime = 10f;
    public float maxTime = 25f;

    void Start()
    {
        Invoke("SpawnVehicleBottom", Random.Range(minTime, maxTime));
        Invoke("SpawnVehicleTop", Random.Range(minTime, maxTime));
    }

    void SpawnVehicleBottom() {
        int vehicleIndex = Random.Range(0, vehicles.Length);
        Instantiate(vehicles[vehicleIndex], spawnPointBottom.position, spawnPointBottom.rotation);
        Invoke("SpawnVehicleBottom", Random.Range(minTime, maxTime));
    }

    void SpawnVehicleTop() {
        int vehicleIndex = Random.Range(0, vehicles.Length);
        Instantiate(vehicles[vehicleIndex], spawnPointTop.position, Quaternion.Euler(0f, 180f, 0f));
        Invoke("SpawnVehicleTop", Random.Range(minTime, maxTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
