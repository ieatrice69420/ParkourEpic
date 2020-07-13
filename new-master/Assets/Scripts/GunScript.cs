using System.Collections;
using System.Collections.Generic;
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
    RaycastHit hit;
    [SerializeField]
    bool isFullAuto;
    bool isReloading;
    public TextMeshProUGUI ammoIcon;
    public float inAccuracy, maxRange, recoilSlowSpeed, recoil, maxRecoil, timerIncreaser, knockBack, shots;
    public mouselooking ml;
    public GameObject bulletHole;
    public CharacterController charCon;
    float moveSpeed, slowSpeed, maxMoveSpeed;
    Vector3 knockBackDir;
    public Transform player;
    ObjectPooler objPooler;

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
        ml.Recoil(10f);
        if (moveSpeed > 0f)
            moveSpeed -= slowSpeed * Time.deltaTime;
        if (moveSpeed < 0f)
            moveSpeed = 0f;
        charCon.Move(knockBackDir * moveSpeed * Time.deltaTime);
    }

    void Fire()
    {
        if (currentMagSize > 0 && !isReloading)
            if (isFullAuto)
                if (Input.GetKey(shoot) && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / fireRate;
                    moveSpeed = maxMoveSpeed;
                    knockBackDir = charCon.transform.forward * -1f;
                    for (int i = 0; i < shots; i++)
                        Shoot();
                }
                else
                if (Input.GetKeyDown(shoot) && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / fireRate;
                    moveSpeed = maxMoveSpeed;
                    knockBackDir = charCon.transform.forward * -1f;
                    for (int i = 0; i < shots; i++)
                        Shoot();
                }
    }

    void ReloadInput()
    {
        if (currentMagSize < magSize && ammo > 0 && !isReloading)
            if (Input.GetKeyDown(reload))
                StartCoroutine(Reload(reloadTime));
        if (currentMagSize == 0 && ammo > 0 && !isReloading)
            if (Input.GetKey(shoot))
                StartCoroutine(Reload(reloadTime));
    }

    void Shoot()
    {
        currentMagSize--;
        if (ml.currentRecoil < maxRecoil)
            ml.StartRecoil(recoil, timerIncreaser);
        Vector3 shootDir = cam.forward + cam.right * Random.Range(-inAccuracy / 2f, inAccuracy / 2f) + cam.up * Random.Range(-inAccuracy, inAccuracy);
        if (Physics.Raycast(cam.position, shootDir, out hit, maxRange))
        {
            target = hit.transform.gameObject.GetComponent<Health>();
            if (target != null)
            {
                if (target != ownHealth)
                {
                    fallOffDis = (hit.point - transform.position).magnitude;
                    damage = baseDamage - fallOffDis * fallOff;
                    target.TakeDamage(damage, hit.point, headShotMultiplier);
                    Debug.Log(target.transform.name);
                }
            }
            else
            {
                RaycastHit bulletHoleHit;
                if (Physics.Raycast(cam.position, shootDir, out bulletHoleHit, maxRange))
                    objPooler.SpawnBulletHole("Bullet Hole", hit.point, hit.normal);
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

    void UpdateAmmoIcon()
    {
        ammoIcon.text = currentMagSize.ToString() + "/" + ammo.ToString();
    }
}