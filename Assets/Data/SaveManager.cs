using UnityEngine;
using static Saving.SavePlayerData;

public class SaveManager : MonoBehaviour
{
    public Health health;
    public WeaponManager weapon;
    public CharacterController chContr;

    void Update()
    {
    	if (Input.GetKey(KeyCode.LeftControl))
    		if (Input.GetKey(KeyCode.S)) ManagerSavePlayer();
    	if (Input.GetKey(KeyCode.O)) ManagerLoadPlayer();
    }

    void ManagerSavePlayer()
    {
    	SavePlayer(health, weapon);
    }

    void ManagerLoadPlayer()
    {
    	PlayerData data = LoadPlayer();

    	health.health = data.health;
    	weapon.selectedWeapon = data.weapon;
        weapon.SelectWeapon();
        chContr.enabled = false;
    	transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        chContr.enabled = true;
        Debug.Log(transform.position);
    }
}
