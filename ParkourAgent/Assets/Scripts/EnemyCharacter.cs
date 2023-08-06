using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyCharacter : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    bool isFiring;
    public float currentSpeed;
    Vector3 lastPos;
    public Animator anim;
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float bulletForce = 100f;
    float animSpeed;

    public float health = 100;
    public GameObject explosionPrefab;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    float velocityRef, velocityRef1;
    public Image healthBar;
    public GameObject healthCanvas;
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
        if(healthBar)
        {
            healthBar.fillAmount = health / 100;
        }

        if(target != null)
        {   
            healthCanvas.SetActive(true);
            
            agent.SetDestination(target.position);
            if(currentSpeed <= 0.1 && (target.position.y - transform.position.y) < 3)
            {
                isFiring = true;
            }
            else
            {
                isFiring = false;
            }
        }
        else
        {
            healthCanvas.SetActive(false);
        }
        if(isFiring == true)
        {
            transform.LookAt(target.position);
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            animSpeed -= Time.deltaTime;
            animSpeed = Mathf.Clamp(animSpeed, -1, 1);
        }
        else
        {
            animSpeed = Mathf.SmoothDamp(animSpeed, currentSpeed / agent.speed, ref velocityRef, 0.1f);
        }
        anim.SetFloat("velocity", animSpeed);
    }

    void FixedUpdate()
    {
        float movmentPerFrame = Vector3.Distance(lastPos, transform.position);
        currentSpeed = movmentPerFrame / Time.deltaTime;
        lastPos = transform.position;
    }
    public void FireAnim()
    {
        if(!isFiring) return;
        Debug.LogError("FIRED BULLET");

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity, transform);

        Ray ray = new Ray(shootingPoint.position, transform.forward);
        bullet.GetComponent<Rigidbody>().velocity = (ray.direction * bulletForce);
        Destroy(bullet, 10);

    }
}
