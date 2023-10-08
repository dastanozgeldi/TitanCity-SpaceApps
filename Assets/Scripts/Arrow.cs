using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        transform.LookAt(target);
        //transform.rotation = Quaternion.Euler(90f, transform.rotation.y, transform.rotation.z);
    }
}
