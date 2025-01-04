using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    public Transform characterOrientation;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = characterOrientation.rotation;
    }
}
