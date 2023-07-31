using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugBullet : Bullet
{
    public GameObject bulletDecalPrefab;
    void OnCollisionEnter(Collision other)
    {   
        // DEAL DAMAGE
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyCharacter>().health -= damage;
        }
        if(other.gameObject.layer == 6 && other.collider.attachedRigidbody == null)
        {
            GameObject decal = Instantiate(bulletDecalPrefab, other.contacts[0].point, Quaternion.LookRotation(other.contacts[0].normal));
            decal.transform.localPosition += decal.transform.forward * 0.02f;
            decal.transform.SetParent(other.transform);
        }

        Destroy(gameObject);

    }
}
