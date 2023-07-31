using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GunManager : MonoBehaviour
{
    public Gun[] gunList;

    public Gun selectedGun;
    public Image reloadCirlce;
    public PlayerMovement controller;
    public void Start()
    {
        selectGun(shotGun ? 0 : 1);
    }
    public Vector3 targetFallingRotation;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    bool shotGun;
    float rotX;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && GameManager.instance.GameStarted)
        {
            shotGun = !shotGun;
            selectGun(shotGun ? 0 : 1);
            UI_Manager.instance.grenadePanel.SetActive(!shotGun);
        }
        reloadCirlce.fillAmount = Mathf.Clamp01(selectedGun.delay / selectedGun.delayBetweenShots);
        if(controller.rb.velocity.y < 0)
        {
            rotX += Time.deltaTime * 0.1f;
            rotX = Mathf.Clamp01(rotX);
            Quaternion nextRotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(targetFallingRotation), rotX);
            transform.localRotation = nextRotation;
        }
        else if(controller.rb.velocity.y >= 0)
        {
            rotX -= Time.deltaTime * 0.6f;
            rotX = Mathf.Clamp01(rotX);
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public void selectGun(int gunID)
    {
        if(selectedGun == gunList[gunID])
            return;

        selectedGun = gunList[gunID];
        foreach (var item in gunList)
        {
            item.gameObject.SetActive(false);
        }
        gunList[gunID].gameObject.SetActive(true);
    }
}
