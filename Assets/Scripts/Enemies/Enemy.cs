using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isHit = false;
    // States :
    // 0 : idle; 1 : rifle idle; 2 : hit; 3 : walk
    [HideInInspector] public Animator anim;
    private Shoot shootScript;
    [SerializeField] private GameObject rifle;

    private void Start() {
        anim = GetComponent<Animator>();
        shootScript = GetComponent<Shoot>();
    }

    void SupprEnemy() {
        gameObject.SetActive(false);
    }

    public void Hit() {
        if (!isHit) {
            anim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
            anim.SetInteger("state", 2);
            Invoke("SupprEnemy", 15f);
        }
        isHit = true;
    }

    public void Shoot() {
        if (!isHit) {
            shootScript.Fire();
        }
    }

    public void AimAt(Transform target) {
        if (!isHit) {
            rifle.transform.LookAt(target);
        }
    }
}
