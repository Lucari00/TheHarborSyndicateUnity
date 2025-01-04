using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drill : MonoBehaviour
{
    [SerializeField] private GameObject explosiveFx;

    private GameObject canvas;
    private Image fillerImage;

    [SerializeField] private float timeToDrill;

    private float timeDrilling;
    private bool isDrilling;

    private Animator doorsAnimator;
    private GameObject gameMaster;

    [SerializeField] private AudioSource explosionSound;
    private AudioSource drillSound;

    void Start()
    {
        canvas = gameObject.GetComponentInChildren<Canvas>().gameObject;
        fillerImage = canvas.GetComponentsInChildren<Image>()[1];
        fillerImage.fillAmount = 0.0f;
        doorsAnimator = transform.root.GetComponent<Animator>();
        gameMaster = GameObject.Find("GameMaster");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrilling) {
            timeDrilling += Time.deltaTime;
            fillerImage.fillAmount = timeDrilling / timeToDrill;
            if (timeDrilling >= timeToDrill) {
                isDrilling = false;
                DrillFinished();
            }
        }
    }

    void DrillFinished() {
        // play fx
        Instantiate(explosiveFx, transform.position, Quaternion.identity);
        canvas.SetActive(false);
        explosionSound.Play();
        gameObject.SetActive(false);
        ExecuteEvents.Execute<ICustomMessageTarget>(gameMaster, null, (x, y) => x.ContainerDrilled());
        OpenDoors();
    }

    void OpenDoors() {
        doorsAnimator.Play("ContainerDoors");
    }

    public void PutDrill() {
        gameObject.SetActive(true);
        isDrilling = true;
        drillSound = GetComponent<AudioSource>();
        drillSound.Play();
    }
}
