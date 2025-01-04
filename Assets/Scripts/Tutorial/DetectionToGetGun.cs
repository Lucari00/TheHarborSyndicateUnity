using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionToGetGun : AbstractTutoClass
{
    public GameObject lightGun;
    public GameObject lightGuards;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            lightGun.SetActive(true);
            lightGun.GetComponent<AudioSource>().Play();
            Invoke("LightGuards", 0.5f);
            gameObject.SetActive(false);
            isFinish = true;
        }
    }

    void LightGuards() {
        lightGuards.SetActive(true);
        lightGuards.GetComponent<AudioSource>().Play();
    }

    //override public void ObjectVisibleThroughWall() {}
    //override public void ObjectHiddenThroughWall() {}
}
