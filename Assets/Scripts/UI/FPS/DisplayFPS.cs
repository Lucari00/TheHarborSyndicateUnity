using UnityEngine;
using TMPro;

public class DisplayFPS : MonoBehaviour {
    public TextMeshProUGUI fpsText;
    public float deltaTime;
    public bool showFPS;

    private void Start() {
        showFPS = true;
    }

    void Update() {
        if (!showFPS) {
            fpsText.text = "";
            return;
        }
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString() + " FPS";
    }

    public void ToggleFPS() {
        showFPS = !showFPS;
    }
}
