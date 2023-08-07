using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class onEnterEvent : MonoBehaviour
{
    public UnityEvent newEvent;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    public bool Entered;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !Entered)
        {
            newEvent.Invoke();
            Entered = true;
        }
    }
}
