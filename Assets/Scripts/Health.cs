using System.Reflection.Emit;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float health = 100f, regenSpeed = 17f, respawnTIme = 3f;
    public HealthSO healthSO;
    public Vector3[] positions;
    public int maxSize;
    public WeaponManager wm;
    public CharacterController playerBody;
    public float slowDuration, slowSpeed;
    public Image image;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (health < 100f)
            Regen();
        if (health > 100f)
            health = 100f;
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        if (!healthSO.isMultiplayer && healthSO.isRealPlayer)
        {
            image.color = new Color(255f, 255f, 255f, (100f - health) * 1.5f);
        }
    }

    void Regen() => health += regenSpeed * Time.deltaTime;

    public void TakeDamage(float amnt, Vector3 hitPos, float headShotMultiplier)
    {
        if (hitPos.y > (healthSO.headShotStart + transform.position).y)
            amnt *= headShotMultiplier;
        health -= amnt;
        if (health <= 0f)
            Die();
    }

    public void SimpleTakeHealth(float amnt)
    {
        health -= amnt;
        if (health <= 0f)
            Die();
    }

    void Die()
    {
        if (healthSO.isMultiplayer)
            StartCoroutine(Respawn(respawnTIme));
        else
        {
            if (healthSO.isRealPlayer)
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            else
                StartCoroutine(SlowTime(slowDuration, slowSpeed));
        }
    }

    IEnumerator Respawn(float duration)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        playerBody.enabled = false;
        yield return new WaitForSeconds(duration);
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        playerBody.enabled = true;
        health = 100f;
        wm.selectedWeapon = 0;
        wm.SelectWeapon();
        transform.position = positions[Random.Range(0, maxSize)];
    }

    IEnumerator SlowTime(float duration, float timeScale)
    {
        Time.timeScale = timeScale;
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
            renderer.enabled = false;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}