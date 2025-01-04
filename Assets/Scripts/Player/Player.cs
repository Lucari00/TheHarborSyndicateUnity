using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Image healthUI;
    [SerializeField] private PostProcessVolume postProcessVolume;
    private int health = 100;
    private RestartGame restartGameScript;
    private GameObject playerCam;
    [SerializeField] private GameObject crosshair;

    void Start() {
        GameObject gameObject = GameObject.Find("RestartGame");
        if (gameObject != null) {
            restartGameScript = gameObject.GetComponent<RestartGame>();
        }
        playerCam = GameObject.Find("PlayerCam");
    }

    public void TakeDamage(int damage) {
        if (health > 0) {
            health -= damage;
        }
        if (health <= 0) {
            Die();
        }
        if(postProcessVolume != null && postProcessVolume.profile != null) {
            ChromaticAberration chromaticAberration = postProcessVolume.profile.GetSetting<ChromaticAberration>();
            if(chromaticAberration != null) {
                chromaticAberration.intensity.value = Mathf.Lerp(0, 1, 1 - health / 100f);
            }
            LensDistortion lensDistortion = postProcessVolume.profile.GetSetting<LensDistortion>();
            if(lensDistortion != null) {
                lensDistortion.intensity.value = Mathf.Lerp(0, -60, 1 - health / 100f);
            }
        }
        healthUI.fillAmount = health / 100f;
    }

    void Die() {
        gameOverText.text = "You died! Press R to restart.";
        gameOverText.color = Color.red;
        gameOverText.gameObject.transform.parent.gameObject.SetActive(true);

        gameObject.SetActive(false);
        Destroy(playerCam.GetComponent<PlayerCam>());
        restartGameScript.isRestarteable = true;
        crosshair.SetActive(false);
    }

}
