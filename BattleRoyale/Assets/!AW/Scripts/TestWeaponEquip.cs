using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestWeaponEquip : NetworkBehaviour {

    public PlayerWeapon testWeapon;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L) && isLocalPlayer)
        {
            GetComponent<WeaponManager>().EquipWeapon(testWeapon, 1);
        }
	}
}
