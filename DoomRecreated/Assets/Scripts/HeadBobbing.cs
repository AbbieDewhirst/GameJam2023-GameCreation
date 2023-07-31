using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public PlayerMovement controller;
    public bool doHeadBobbing = true;

    public Transform targetTransfrom;
    public Vector3 offset = Vector3.zero;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(controller.currentSpeed > 1f && doHeadBobbing && controller.grounded && !controller.crouching)
        {
            HandleHeadBobbing();
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //defaultX = targetTransfrom.localPosition.x;
        //defaultY = targetTransfrom.localPosition.y;
    }

    float defaultX, defaultY;
    float timer;
    [SerializeField] float xBob, yBob, x_headBobAmount, y_headbobAmount, bobSpeed;
    void HandleHeadBobbing()
    {
        timer += Time.deltaTime * bobSpeed;

        xBob = Mathf.Cos(timer) * x_headBobAmount;
        yBob = Mathf.Sin(timer) * y_headbobAmount;

        targetTransfrom.transform.localPosition = new Vector3(( defaultX + xBob ) * controller.currentSpeed, ( defaultY + yBob) * controller.currentSpeed, targetTransfrom.localPosition.z) + offset;
    }

    
}
