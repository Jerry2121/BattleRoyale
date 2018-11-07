using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    [SerializeField]
    string weaponLayerName = "Weapon";
    [SerializeField]
    PlayerWeapon primaryWeapon;
    [SerializeField]
    Transform weaponHolder;

    private PlayerWeapon currentWeapon;
    private WeaponGraphics currentGraphics;

	// Use this for initialization
	void Start () {
        EquipWeapon(primaryWeapon);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void EquipWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;

        GameObject weaponIns = Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);
        currentGraphics = weaponIns.GetComponent<WeaponGraphics>();
        if(currentGraphics == null)
        {
            Debug.LogError("WeaponManager -- EquipWeapon: There is no WeaponGraphics on the " + weaponIns.name +" weapon object!");
        }

        if (isLocalPlayer)
        {
            Utility.SetLayerRecursively(weaponIns, LayerMask.NameToLayer(weaponLayerName));
        }

    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }

}
