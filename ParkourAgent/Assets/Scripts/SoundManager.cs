using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager instance;

    public GameObject soundSourcePrefab;
    public AudioClip shootingGLClip, jumpClip, explosionClip, shotgunClip, noReloadClip, amoReloadClip, satisfyingClip;
    public bool isMuted;
    public AudioSource walkingSource, windSource;
    public bool isWalking;
    public PlayerMovement controller;



    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        isWalking = controller.currentSpeed > 0.5f && controller.grounded && !controller.crouching && !isMuted;
        if(isWalking)
        {   
            if(walkingSource.isPlaying == false) walkingSource.Play();
            walkingSource.volume += Time.deltaTime * 10;
            walkingSource.volume = Mathf.Clamp01(walkingSource.volume);
        }
        else
        {
            walkingSource.volume -= Time.deltaTime * 10;
            walkingSource.volume = Mathf.Clamp01(walkingSource.volume);
        }
    }
    public void playGLshooting()
    {
        if(isMuted) return;
        GameObject shootingSound = Instantiate(soundSourcePrefab, transform);
        shootingSound.GetComponent<AudioSource>().clip = shootingGLClip;
        shootingSound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1f);
        shootingSound.GetComponent<AudioSource>().volume = 0.2f;
        shootingSound.GetComponent<AudioSource>().Play();
        Destroy(shootingSound, shootingGLClip.length);
    }
    public void playShootShotGun()
    {
        if(isMuted) return;
        GameObject shootingSound = Instantiate(soundSourcePrefab, transform);
        shootingSound.GetComponent<AudioSource>().clip = shotgunClip;
        shootingSound.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1f);
        shootingSound.GetComponent<AudioSource>().volume = 0.2f;
        shootingSound.GetComponent<AudioSource>().Play();
        Destroy(shootingSound, shotgunClip.length);
    }
    public void playJump()
    {
        if(isMuted) return;
        GameObject jumpSound = Instantiate(soundSourcePrefab, transform);
        jumpSound.GetComponent<AudioSource>().clip = jumpClip;
        jumpSound.GetComponent<AudioSource>().volume = 0.6f;
        jumpSound.GetComponent<AudioSource>().Play();
        Destroy(jumpSound, jumpClip.length);
    }
    public void playExplosion(Vector3 location)
    {
        if(isMuted) return;
        GameObject shootingSound = Instantiate(soundSourcePrefab, location, Quaternion.identity);
        shootingSound.GetComponent<AudioSource>().clip = explosionClip;
        shootingSound.GetComponent<AudioSource>().spatialBlend = 0.98f;
        shootingSound.GetComponent<AudioSource>().Play();
        Destroy(shootingSound, explosionClip.length);
    }
    public void playNoReloadSound()
    {
        if(isMuted) return;
        GameObject noReloadSound = Instantiate(soundSourcePrefab, transform);
        noReloadSound.GetComponent<AudioSource>().clip = noReloadClip;
        noReloadSound.GetComponent<AudioSource>().volume = 0.6f;
        noReloadSound.GetComponent<AudioSource>().Play();
        Destroy(noReloadSound, noReloadClip.length);
    }
    public void playAmoReload()
    {
        if(isMuted) return;
        GameObject playAmoReload = Instantiate(soundSourcePrefab, transform);
        playAmoReload.GetComponent<AudioSource>().clip = amoReloadClip;
        playAmoReload.GetComponent<AudioSource>().volume = 0.6f;
        playAmoReload.GetComponent<AudioSource>().Play();
        Destroy(playAmoReload, amoReloadClip.length);
    }
    public void playSatisfyingSound()
    {
        if(isMuted) return;
        GameObject playSatisfyingObj = Instantiate(soundSourcePrefab, transform);
        playSatisfyingObj.GetComponent<AudioSource>().clip = satisfyingClip;
        playSatisfyingObj.GetComponent<AudioSource>().volume = 0.6f;
        playSatisfyingObj.GetComponent<AudioSource>().Play();
        Destroy(playSatisfyingObj, satisfyingClip.length);
    }
}
