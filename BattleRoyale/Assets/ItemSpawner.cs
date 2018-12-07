using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemSpawner : MonoBehaviour {
    [Header("Spawner Type")]
    public bool WeaponSpawner;
    public bool AmmoSpawner;
    [Header("AmmoSpawners")]
    public GameObject AmmoSpawner1;
    public GameObject AmmoSpawner2;
    [Header("Weapon Bools")]
    public bool canspawnItem;
    public bool ran;
    public bool Weapon1;
    public bool Weapon2;
    public bool Weapon3;
    public bool Weapon4;
    public bool Weapon5;
    public bool Weapon6;
    private int d1;

    NetworkManager networkManager;
    NetworkDiscoveryScript networkDiscoveryScript;

    // Use this for initialization
    void Start () {
        canspawnItem = true;
        ran = false;

        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (canspawnItem && networkDiscoveryScript.isServer)
        {
            d1 = Random.Range(0, 7);
            if (d1 == 1 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon1 = true;
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon1, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
                NetworkServer.Spawn(GO);
            }
            if (d1 == 2 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon2 = true;
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon2, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
                NetworkServer.Spawn(GO);
            }
            if (d1 == 3 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon3 = true;
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon3, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
                NetworkServer.Spawn(GO);
            }
            if (d1 == 4 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon4 = true;
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon4, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
                NetworkServer.Spawn(GO);
            }
            if (d1 == 5 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon5 = true;
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon5, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
                NetworkServer.Spawn(GO);
            }
            if (d1 == 6 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && WeaponSpawner && canspawnItem && !ran)
            {
                Weapon6 = true;
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon6, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                ran = true;
                NetworkServer.Spawn(GO);
            }
            //Ammo
            if (Weapon1 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawner1.transform.position.x, AmmoSpawner1.transform.position.y, AmmoSpawner1.transform.position.z), Quaternion.identity);
                GameObject GO2 = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawner2.transform.position.x, AmmoSpawner2.transform.position.y, AmmoSpawner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
                NetworkServer.Spawn(GO);
                NetworkServer.Spawn(GO2);
            }
            if (Weapon2 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo2, new Vector3(AmmoSpawner1.transform.position.x, AmmoSpawner1.transform.position.y, AmmoSpawner1.transform.position.z), Quaternion.identity);
                GameObject GO2 = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo2, new Vector3(AmmoSpawner2.transform.position.x, AmmoSpawner2.transform.position.y, AmmoSpawner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
                NetworkServer.Spawn(GO);
                NetworkServer.Spawn(GO2);
            }
            if (Weapon3 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawner1.transform.position.x, AmmoSpawner1.transform.position.y, AmmoSpawner1.transform.position.z), Quaternion.identity);
                GameObject GO2 = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawner2.transform.position.x, AmmoSpawner2.transform.position.y, AmmoSpawner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
                NetworkServer.Spawn(GO);
                NetworkServer.Spawn(GO2);
            }
            if (Weapon4 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo3, new Vector3(AmmoSpawner1.transform.position.x, AmmoSpawner1.transform.position.y, AmmoSpawner1.transform.position.z), Quaternion.identity);
                GameObject GO2 = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo3, new Vector3(AmmoSpawner2.transform.position.x, AmmoSpawner2.transform.position.y, AmmoSpawner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
                NetworkServer.Spawn(GO);
                NetworkServer.Spawn(GO2);
            }
            if (Weapon5 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo2, new Vector3(AmmoSpawner1.transform.position.x, AmmoSpawner1.transform.position.y, AmmoSpawner1.transform.position.z), Quaternion.identity);
                GameObject GO2 = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo2, new Vector3(AmmoSpawner2.transform.position.x, AmmoSpawner2.transform.position.y, AmmoSpawner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
                NetworkServer.Spawn(GO);
                NetworkServer.Spawn(GO2);
            }
            if (Weapon6 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                GameObject GO = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawner1.transform.position.x, AmmoSpawner1.transform.position.y, AmmoSpawner1.transform.position.z), Quaternion.identity);
                GameObject GO2 = Instantiate(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, new Vector3(AmmoSpawner2.transform.position.x, AmmoSpawner2.transform.position.y, AmmoSpawner2.transform.position.z), Quaternion.identity);
                canspawnItem = false;
                NetworkServer.Spawn(GO);
                NetworkServer.Spawn(GO2);
            }
        }
	}
}
