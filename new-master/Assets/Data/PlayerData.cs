using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public float health;
    public int weapon;
    public int level;
    public float[] position;

    public PlayerData(Health healthScript, WeaponManager weaponScript)
    {
    	health = healthScript.health;

    	weapon = weaponScript.selectedWeapon;

    	level = SceneManager.GetActiveScene().buildIndex;

    	position = new float[3];
    	position[0] = healthScript.transform.position.x;
    	position[1] = healthScript.transform.position.y;
    	position[2] = healthScript.transform.position.z;
    }
}
