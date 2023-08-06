using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_GrenadL : Gun
{
    // Start is called before the first frame update
    public int currentBulletCount = 3;
    public GameObject addAmoPrefab;
    public Transform addAmoAnimParent;
    
    
    void Start()
    {
        delay = delayBetweenShots;
        UI_Manager.instance.bulletCountTxt.text = currentBulletCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime;
        if(delay <= 0 && Input.GetMouseButtonDown(0) && GameManager.instance.GameStarted && !UI_Manager.instance.isPaused && currentBulletCount > 0)
        {

            //SHOOT
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            anim.SetTrigger("Shoot");
            CameraShake.instance.shake(0.5f);
            SoundManager.instance.playGLshooting();
            float x = Screen.width / 2;
            float y = Screen.height / 2;
                        
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y, 0));
            bullet.GetComponent<Rigidbody>().velocity = (ray.direction * bulletSpeed) + controller.rb.velocity;

            Destroy(bullet, 10);

            currentBulletCount -= 1;
            UI_Manager.instance.bulletCountTxt.text = currentBulletCount.ToString();
            delay = delayBetweenShots;
        }
        if(Input.GetMouseButtonDown(0) && currentBulletCount <= 0 && delay <= 0 && !UI_Manager.instance.isPaused)
        {
            SoundManager.instance.playNoReloadSound();
        }
    }

    public void addAmmo(int count)
    {
        currentBulletCount += count;
        UI_Manager.instance.bulletCountTxt.text = currentBulletCount.ToString();
        SoundManager.instance.playAmoReload();
        GameObject addReload = Instantiate(addAmoPrefab, addAmoAnimParent);
        Destroy(addReload, 1);
    }
}
