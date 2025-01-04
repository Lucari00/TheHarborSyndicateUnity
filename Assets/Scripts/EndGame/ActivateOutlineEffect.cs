using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOutlineEffect : MonoBehaviour
{

    [SerializeField] private Camera oldCamera;
    private bool isFinished = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cakeslice.OutlineEffect outlineEffect = gameObject.GetComponent<cakeslice.OutlineEffect>();
        if (gameObject.activeSelf && outlineEffect is null && !isFinished) {  
            Destroy(oldCamera.GetComponent<cakeslice.OutlineEffect>());
            Invoke("AddOutlineEffect", 0.1f);
            isFinished = true;
        }
    }

    void AddOutlineEffect() {
        gameObject.AddComponent<cakeslice.OutlineEffect>();
    }
}
