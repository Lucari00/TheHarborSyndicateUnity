using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeletedObject : MonoBehaviour
{
    [SerializeField] private float timeBeforeDeletion = 30f;
    void Start()
    {
        Invoke("SupprObject", timeBeforeDeletion);
    }

    void SupprObject() {
        Destroy(gameObject);
    }
}
