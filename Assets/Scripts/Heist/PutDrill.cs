using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PutDrill : MonoBehaviour
{
    private bool isInRange; 
    private GameObject gameMaster;
    [SerializeField] private TextMeshProUGUI textKey;

    private void Start() {
        isInRange = false;
        gameMaster = GameObject.Find("GameMaster");
    }

    void Update() {
        if (isInRange && Input.GetKeyDown(KeyCode.E)) {
            Drill drill = FindFirstObjectByType<Drill>(FindObjectsInactive.Include);
            drill.PutDrill();
            ExecuteEvents.Execute<ICustomMessageTarget>(gameMaster, null, (x, y) => x.GoDrillContainterFinished());
            textKey.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            isInRange = true;
            textKey.text = "Press E to put the drill";
            textKey.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            isInRange = false;
            textKey.gameObject.SetActive(false);
        }
    }
}
