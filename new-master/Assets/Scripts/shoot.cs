using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public float fireRate, nextTimeToFire;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform firePoint, player;
    Rigidbody rb;
    [SerializeField]
    public float speed = 10;

    void Start()
    {
        projectile = Resources.Load("CannonBall") as GameObject;
    }

    void Update()
    {
        transform.LookAt(player.position);

        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject cannonBall = Instantiate(projectile) as GameObject;
        cannonBall.transform.position = firePoint.position;
        cannonBall.transform.LookAt(player.position);
        rb = cannonBall.GetComponent<Rigidbody>();
        rb.velocity = cannonBall.transform.forward * speed;
    }
}