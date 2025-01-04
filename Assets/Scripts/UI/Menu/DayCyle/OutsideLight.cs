using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideLight : MonoBehaviour
{
    private Light _light;
    void Start()
    {
        _light = GetComponent<Light>();
    }

    public void TurnOffLight() {
        _light.enabled = false;
    }

    public void TurnOnLight() {
        _light.enabled = true;
    }
}
