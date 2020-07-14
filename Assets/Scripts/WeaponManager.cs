using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public int selectedWeapon;
    [SerializeField]
    Transform weaponHolder;
    public KeyCode pickUpCode = KeyCode.E;
    PickUpableMonoB pickUpAble;
    public TextMeshProUGUI text;

    void Start() => SelectWeapon();

    void OnTriggerStay(Collider other)
    {
        pickUpAble = other.gameObject.GetComponent<PickUpableMonoB>();
        if (pickUpAble != null)
        {
            text.gameObject.SetActive(true);
            text.text = "Press E To Pick Up              " + pickUpAble.sO.name;
            if (Input.GetKeyDown(pickUpCode))
            {
                text.gameObject.SetActive(false);
                selectedWeapon = pickUpAble.sO.pickUpIndex;
                SelectWeapon();
                Destroy(other.gameObject);
	        }
        }
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in weaponHolder)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                GunScript gs = weapon.GetComponent<GunScript>();
                if (gs != null && pickUpAble != null)
                {
                	gs.ammo = pickUpAble.sO.ammo;
                }
            }
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}