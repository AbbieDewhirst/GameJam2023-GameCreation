using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [HideInInspector] public float delay;
    public float delayBetweenShots;
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float bulletSpeed = 100;
    public Animator anim;
    public PlayerMovement controller;

    void OnEnable()
    {
        delay = delayBetweenShots;
    }
}
