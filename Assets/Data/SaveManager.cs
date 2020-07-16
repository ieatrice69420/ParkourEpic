using UnityEngine;
using Saving;

public class SaveManager : MonoBehaviour
{
    public Health health;
    public WeaponManager weapon;
    public CharacterController chContr;

    void Update()
    {
    	if (Input.GetKey(KeyCode.LeftControl))
    		if (Input.GetKey(KeyCode.S)) SavePlayer();
    	if (Input.GetKey(KeyCode.O)) LoadPlayer();
    }

    public void SavePlayer()
    {
    	SaveSystem.SavePlayer(health, weapon);
    }

    public void LoadPlayer()
    {
    	PlayerData data = SaveSystem.LoadPlayer();

    	health.health = data.health;
    	weapon.selectedWeapon = data.weapon;
        weapon.SelectWeapon();
        chContr.enabled = false;
    	transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        chContr.enabled = true;
        Debug.Log(transform.position);
    }
}
