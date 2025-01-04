using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoat : MonoBehaviour
{
    [HideInInspector] public bool enableMove;
    private AudioSource boatSound;

    void Start()
    {
        enableMove = false;
        boatSound = GetComponent<AudioSource>();
    }

    private void FixedUpdate() {
        if(enableMove) {
            transform.position += new Vector3(0.1f, 0, 0);
        }
    }

    public void PlaySound() {
        if (boatSound != null && !boatSound.isPlaying) {
            boatSound.Play();
        }
    }
}
