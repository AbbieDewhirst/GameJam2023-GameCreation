using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTriggerEnter : MonoBehaviour
{
    EnemyCharacter character;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        character = transform.parent.GetComponent<EnemyCharacter>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(character.target != null) return;
        if(other.gameObject.CompareTag("Player"))
        {
            character.target = other.transform;
            Debug.Log("Enemy Found Player");
        }
    }
}
