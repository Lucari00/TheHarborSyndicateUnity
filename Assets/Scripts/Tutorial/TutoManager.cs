using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutoManager : MonoBehaviour
{
    public Dictionary<int, AbstractTutoClass> tutoScripts;
    private int actualTuto;
    private bool tutoEnded;
    private TextMeshProUGUI missionInformation;
    // Start is called before the first frame update
    void Start()
    {
        missionInformation = GameObject.Find("MissionInformation").GetComponent<TextMeshProUGUI>();

        tutoEnded = false;
        actualTuto = 0;
        tutoScripts = new Dictionary<int, AbstractTutoClass>();

        Transform[] children = GetComponentsInChildren<Transform>();

        int i = 0;

        foreach (Transform child in children) {
            if (child != null) {
                AbstractTutoClass script = child.GetComponent<AbstractTutoClass>();
                if (script != null) {
                    tutoScripts.Add(i, script);
                    i++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutoEnded && tutoScripts[actualTuto].isFinish) {
            tutoScripts[actualTuto].ObjectHiddenThroughWall();
            Debug.Log("Tuto finished : " + tutoScripts[actualTuto]);
            actualTuto += 1;
            if (actualTuto < tutoScripts.Count) {
                Debug.Log("Tuto started : " + tutoScripts[actualTuto]);
                tutoScripts[actualTuto].gameObject.gameObject.SetActive(true);
                tutoScripts[actualTuto].ObjectVisibleThroughWall();
                missionInformation.text = tutoScripts[actualTuto].missionInformationText;
            } else {
                tutoEnded = true;
            }
        }
    }
}
