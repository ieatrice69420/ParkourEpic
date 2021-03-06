﻿using System.Collections;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{
    public float baseDamage, fireRate, fallOff = .2f, fallOffDis, reloadTime, headShotMultiplier;
    float nextTimeToFire, damage, distance;
    public KeyCode shoot = KeyCode.Mouse0, reload = KeyCode.R;
    public Transform cam;
    public Health target, ownHealth;
    public int ammo, magSize, currentMagSize;
    [SerializeField]
    bool isFullAuto;
    bool isReloading;
    public TextMeshProUGUI ammoIcon;
    public float inAccuracy, maxRange, recoilSlowSpeed, recoil, maxRecoil, timerIncreaser, knockBack;
    [SerializeField]
    int shots = 1;
    public mouselooking ml;
    public GameObject bulletHole;
    public CharacterController charCon;
    float moveSpeed, slowSpeed, maxMoveSpeed;
    Vector3 knockBackDir;
    public Transform player;
    ObjectPooler objPooler;
    [SerializeField]
    LayerMask lm;

    void OnEnable()
    {
        objPooler = ObjectPooler.instance;
        StartCoroutine(Reload(reloadTime));
    }

    void Update()
    {
        Fire();
        ReloadInput();
        UpdateAmmoIcon();
        if (moveSpeed > 0f)
        {
            moveSpeed -= slowSpeed * Time.deltaTime;
            charCon.Move(knockBackDir * moveSpeed * Time.deltaTime);
        }
        if (moveSpeed < 0f) moveSpeed = 0f;
    }

    void Fire()
    {
        if (currentMagSize > 0 && !isReloading)
            if (isFullAuto)
            {
                if (Input.GetKey(shoot) && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / fireRate;
                    moveSpeed = maxMoveSpeed;
                    knockBackDir = charCon.transform.forward * -1f;
                    for (int i = 0; i < shots; i++) Shoot();
                }
            }
            else if (Input.GetKeyDown(shoot) && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                moveSpeed = maxMoveSpeed;
                knockBackDir = charCon.transform.forward * -1f;
                for (int i = 0; i < shots; i++) Shoot();
            }
    }

    void ReloadInput()
    {
        if (currentMagSize < magSize && ammo > 0 && !isReloading)
            if (Input.GetKeyDown(reload)) StartCoroutine(Reload(reloadTime));
        if (currentMagSize == 0 && ammo > 0 && !isReloading)
            if (Input.GetKey(shoot)) StartCoroutine(Reload(reloadTime));
    }

    void Shoot()
    {
        currentMagSize--;
        Vector3 shootDir = cam.forward /* + (cam.right * Random.Range(-inAccuracy, inAccuracy)) + cam.up * Random.Range(-inAccuracy, inAccuracy) */;
        RaycastHit[] hits = Physics.RaycastAll(cam.position, shootDir, maxRange);
        foreach (RaycastHit hit in hits)
        {
            target = hit.collider.GetComponent<Health>();
            Debug.Log(hit.transform.name);
            if (target != null)
            {
                Debug.Log(1);
                if (target != ownHealth)
                {
                    Debug.Log(2);
                    fallOffDis = (hit.point - transform.position).magnitude;
                    damage = baseDamage - fallOffDis * fallOff;
                    target.TakeDamage(damage, hit.point, headShotMultiplier);
                    Debug.Log(target.transform.name);
                    break;
                }
            }
            else
            {
                Debug.Log(3);
                if (hit.transform.gameObject.layer != lm) objPooler.SpawnBulletHole("Bullet Hole", hit.point, hit.normal);
            }
        }
    }

    public IEnumerator Reload(float duration)
    {
        isReloading = true;
        yield return new WaitForSeconds(duration);
        if (ammo >= magSize)
        {
            ammo -= magSize - currentMagSize;
            currentMagSize = magSize;
        }
        else
        {
            currentMagSize = ammo;
            ammo = 0;
            Debug.Log("out of ammo");
        }
        isReloading = false;
    }

    void UpdateAmmoIcon() => ammoIcon.text = currentMagSize.ToString() + "/" + ammo.ToString();
}
