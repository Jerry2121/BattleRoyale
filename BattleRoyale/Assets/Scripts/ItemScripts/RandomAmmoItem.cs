using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RandomAmmoItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (NetworkManager.singleton.GetComponent<NetworkDiscoveryScript>().isServer)
        {
            int chance = Random.Range(1, 4);
            GameObject ammo;
            if (chance == 1)
                ammo = GameManager.Instance.gameManagerScript.lightAmmo;
            else if (chance == 2)
                ammo = GameManager.Instance.gameManagerScript.heavyAmmo;
            else
                ammo = GameManager.Instance.gameManagerScript.mediumAmmo;

            Utility.InstantiateOverNetwork(ammo, transform.position, Quaternion.identity);
        }
	}
}
