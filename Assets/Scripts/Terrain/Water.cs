using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player entered water");
            Player playerScript = other.transform.parent.GetComponent<Player>();
            Debug.Log(playerScript == null);
            if (playerScript != null) {
                playerScript.TakeDamage(100);
            }
        }
        else if (other.CompareTag("Vehicle")) {
            //if not sea vehicle, destroy vehicle and kill player
            VehicleController vehicleScript = other.GetComponent<VehicleController>();
            if (vehicleScript is WaterVehicleController) {
                return;
            }
            if (vehicleScript != null && vehicleScript.player != null) {
                vehicleScript.player.GetComponent<Player>().TakeDamage(100);
                vehicleScript.player.GetComponent<PlayerMovement>().GetOutOfVehicle(vehicleScript, vehicleScript.exitPosition);
                Destroy(other.gameObject, 2f);
            }
        }
    }
}
