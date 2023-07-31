using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_ShotGun : Gun
{

    void Start()
    {
        delay = delayBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if(delay <= 0 && Input.GetMouseButtonDown(0) && GameManager.instance.GameStarted && !UI_Manager.instance.isPaused)
        {

            //SHOOT
            List<GameObject> bullets = new List<GameObject>();
            GameObject bullet1 = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            GameObject bullet2 = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            GameObject bullet3 = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            GameObject bullet4 = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            bullets.Add(bullet1);
            bullets.Add(bullet2);
            bullets.Add(bullet3);
            bullets.Add(bullet4);


            foreach (GameObject item in bullets)
            {
                float x = Screen.width / 2;
                float y = Screen.height / 2;

                Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
                float randomX = Random.Range(-4f, 4f);
                float randomY = Random.Range(-4f, 4f);
                Vector3 randomVector = new Vector3(randomX, randomY, 0);
                item.GetComponent<Rigidbody>().velocity = (ray.direction * bulletSpeed) + controller.rb.velocity + randomVector;
                Destroy(item, 10);
                
            }


            anim.SetTrigger("Shoot");
            CameraShake.instance.shake(0.5f);
            SoundManager.instance.playShootShotGun();
            delay = delayBetweenShots;
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
}
