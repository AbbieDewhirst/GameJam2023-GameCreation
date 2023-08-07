using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    public float explostionForce;
    public float explosionRad;
    public GameObject explosionPrefab;
    void OnCollisionEnter(Collision other)
    {   
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRad);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject.CompareTag("Enemy"))
            {
                colliders[i].gameObject.GetComponent<EnemyCharacter>().health -= damage;
            }
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>();

            if(!rb)
                continue;
            


            
            Vector3 direction  = (colliders[i].transform.position - transform.position).normalized;
            Debug.Log(rb.name);
            rb.AddForce(direction * explostionForce, ForceMode.Impulse);
        }

        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        CameraShake.instance.shake(0.5f);
        SoundManager.instance.playExplosion(transform.position);
        Destroy(gameObject);
    }
}
