using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponItem : NetworkBehaviour {

    public PlayerWeapon weapon;
    NetworkIdentity networkIdentity;

	// Use this for initialization
	void Start () {
        networkIdentity = GetComponent<NetworkIdentity>();
        
        GameObject gfx = Instantiate(weapon.graphics, this.transform, false);
        this.transform.localRotation = Quaternion.Euler(weapon.graphics.GetComponent<WeaponGraphics>().rotationOffset);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EquipWeapon(Player _player, string _playerID)
    {
        Debug.Log("WeaponItem -- EquipWeapon");

        //Commands can only be called on player objects, so call on the player
        _player.itemInteractions.CmdEquipWeaponFromItem(netId);
    }
    
    public void OnWeaponEquip(string _playerID)
    {
        Debug.Log("WeaponItem -- OnWeaponEquip");
        Player player = GameManager.GetPlayer(_playerID);
        WeaponManager weaponManager = player.gameObject.GetComponent<WeaponManager>();
        weaponManager.EquipWeapon(weapon, weaponManager.selectedWeapon);
        Destroy(this.gameObject);
    }

}
