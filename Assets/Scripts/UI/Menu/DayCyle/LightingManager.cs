using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour {
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] private float TimeOfDay;

    [SerializeField] private GameObject parentOfLamp;
    private List<OutsideLight> lights;

    void Start () {
        // je veux recupérer le component OutsideLight de chaque enfant de chaque enfant de parentOfLamp
        lights = new List<OutsideLight>();
        foreach (Transform child in parentOfLamp.transform) {
            foreach (Transform child2 in child) {
                lights.Add(child2.GetComponent<OutsideLight>());
            }
        }
    }


    private void Update() {
        if (Preset == null)
            return;

        if (Application.isPlaying) {
            TimeOfDay += Time.deltaTime;
            TimeOfDay %= 24; //Modulus to ensure always between 0-24
            UpdateLighting(TimeOfDay / 24f);
            if (TimeOfDay < 8 || TimeOfDay > 19) {
                foreach (OutsideLight light in lights) {
                    if (light != null) {
                        light.TurnOnLight();
                    }
                }
            } else {
                foreach (OutsideLight light in lights) {
                    if (light != null) { 
                        light.TurnOffLight();
                    }
                }
            }
        } else {
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent) {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color
        if (DirectionalLight != null) {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate() {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null) {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights) {
                if (light.type == LightType.Directional) {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}