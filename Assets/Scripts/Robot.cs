using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Vector3 rotationVector;

    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime);
    }
}
