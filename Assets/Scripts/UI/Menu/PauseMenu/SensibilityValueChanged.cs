using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SensibilityValueChanged : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI placeholderText;
    public TextMeshProUGUI valueText;

    private void Update() {
        placeholderText.text = slider.value.ToString();
    }

    public void OnValueChanged() {
        Debug.Log("Value changed");
        if (valueText != null) {
            int number = int.Parse(valueText.text.ToString());
            if (number < 0) {
                slider.value = 0;
            } else if (number > 400) {
                slider.value = 400;
            } else {
                slider.value = number;
            }
        }
    }
}
