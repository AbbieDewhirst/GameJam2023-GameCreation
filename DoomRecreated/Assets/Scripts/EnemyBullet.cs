using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    void OnCollisionEnter(Collision other)
    {   
        // DEAL DAMAGE
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.Damage(damage);
        }
        

        Destroy(gameObject);

    }
}
