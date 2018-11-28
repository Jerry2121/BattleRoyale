﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestWeaponEquip : NetworkBehaviour {

    public PlayerWeapon testWeapon;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L) && isLocalPlayer)
        {
            WeaponManager weaponManager = GetComponent<WeaponManager>();
            weaponManager.EquipWeaponLocal(testWeapon, weaponManager.selectedWeapon);
            CmdOnTestWeaponEquip();
        }
	}

    [Command]
    void CmdOnTestWeaponEquip()
    {
        RpcOnTestWeaponEquip();
    }

    [ClientRpc]
    void RpcOnTestWeaponEquip()
    {
        if(isLocalPlayer == false)
        {
            WeaponManager weaponManager = GetComponent<WeaponManager>();
            weaponManager.EquipWeaponLocal(testWeapon, weaponManager.selectedWeapon);
        }
    }
}
