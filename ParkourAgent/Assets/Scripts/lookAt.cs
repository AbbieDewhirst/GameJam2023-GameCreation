using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{
    public Transform cam;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        if(cam != null)
        {
            transform.LookAt(cam);
        }
    }
}
