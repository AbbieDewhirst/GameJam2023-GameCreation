using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject explosionPrefab;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(health <= 0)
        {
            // DIE
            Debug.Log("Enemy Down");
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            CameraShake.instance.shake(0.5f);
            SoundManager.instance.playExplosion(transform.position);
            Destroy(gameObject);
        }
    }
}
