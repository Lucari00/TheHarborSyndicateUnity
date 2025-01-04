using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : WheeledVehicleController
{
    [Header("Truck Components")]
    public TruckDoors truckDoors;
    [SerializeField] private GameObject centerOfMass;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        transform.GetComponent<Rigidbody>().centerOfMass = centerOfMass.transform.localPosition;
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
        if (player == null) {
            return;
        }
    }

    private void DeactivateTriggerDoors() {
        if (player == null) {
            truckDoors.DeactivateTrigger();
        }
    }

    private void ActivateTriggerDoors() {
        if(player != null) {
            truckDoors.ActivateTrigger();
        }
    }
}
