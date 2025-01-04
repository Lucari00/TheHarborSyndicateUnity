using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : WheeledVehicleController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (player == null) {
            return;
        }
    }
}
