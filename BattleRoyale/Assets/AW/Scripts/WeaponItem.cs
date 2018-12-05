using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponItem : NetworkBehaviour {

    public PlayerWeapon weapon;

	// Use this for initialization
	void Start () {
        GameObject gfx = Instantiate(weapon.graphics, this.transform, false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EquipWeapon(WeaponManager _weaponManager, string _playerID)
    {
        Debug.Log("WeaponItem -- EquipWeapon");

        //WeaponManager weaponManager = GetComponent<WeaponManager>();
        //weaponManager.EquipWeaponLocal(weapon, weaponManager.selectedWeapon);
        CmdOnWeaponEquip(_playerID);
    }

    [Command]
    void CmdOnWeaponEquip(string _playerID)
    {
        RpcOnWeaponEquip(_playerID);
    }

    [ClientRpc]
    void RpcOnWeaponEquip(string _playerID)
    {
        Debug.Log("WeaponItem -- RpcEquipWeapon");
        Player player = GameManager.GetPlayer(_playerID);
        WeaponManager weaponManager = player.gameObject.GetComponent<WeaponManager>();
        weaponManager.EquipWeaponLocal(weapon, weaponManager.selectedWeapon);
        Destroy(this.gameObject);
    }

}
