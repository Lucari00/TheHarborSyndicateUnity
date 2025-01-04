using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OpenGarageDoors : MonoBehaviour
{
    private Animator animator;

    public float timeToOpenDoor;

    public AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor() {
        PlaySound();
        animator.Play("GarageDoorAnimation");
    }

    void PlaySound() {
        if (audioSource != null) {
            audioSource.Play();
        }
    }

    void StopSound() {
        if (audioSource != null && audioSource.isPlaying) {
            audioSource.Stop();
        }
    }

}
