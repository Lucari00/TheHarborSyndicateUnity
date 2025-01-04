using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GameMaster : MonoBehaviour, ICustomMessageTarget
{
    [SerializeField] private TextMeshProUGUI textMissionInformation;
    [SerializeField] private EnemyManager enemyManagerScript;
    [SerializeField] private DetectEndGame endScript;
    [SerializeField] private GameObject directionArrows;

    void Start() {
        textMissionInformation.text = "Go to the container, highlighted in red!";
    }

    public void GoDrillContainterFinished() {
        textMissionInformation.text = "Wait for the drilling to finish! Defend it!";

        enemyManagerScript.SpawnGuards(5, new Vector3(47.8300018f, 0, -47.2439995f), new Vector3(0, 0, 2), Quaternion.identity);
        
    }

    public void ContainerDrilled() {
        textMissionInformation.text = "The container has been drilled, take the car!";
    }

    public void VehicleEntered() {
        textMissionInformation.text = "Follow the arrows to the exit! It's highlighted in red!";
        endScript.ActivateDetect();
        directionArrows.SetActive(true);
    }
}
