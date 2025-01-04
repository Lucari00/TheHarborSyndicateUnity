using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class FenceDoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] fenceDoors;
    private Animator[] fenceDoorsAnimators;
    [SerializeField] private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        fenceDoorsAnimators = new Animator[fenceDoors.Length];
        for(int i = 0; i < fenceDoors.Length; i++) {
            fenceDoorsAnimators[i] = fenceDoors[i].GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("StolenCar")) {
            foreach(Animator animator in fenceDoorsAnimators) {
                if (animator != null) {
                    animator.SetTrigger("open");
                }
            }
            sound.Play();
            Destroy(gameObject, 4f);
        }
    }
}
