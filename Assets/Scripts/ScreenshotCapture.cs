using UnityEngine;

public class ScreenshotCapture : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            string fileName = "screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

            string filePath = System.IO.Path.Combine(Application.persistentDataPath, fileName);
            ScreenCapture.CaptureScreenshot(filePath);

            Debug.Log("Screenshot captured: " + filePath);
        }
    }
}
