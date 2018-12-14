using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponItemSpawner : MonoBehaviour {
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
            if (d1 == 1 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon1 = true;
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon1, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 2 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon2 = true;
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon2, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 3 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon3 = true;
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon3, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 4 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon4 = true;
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon4, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 5 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon5 = true;
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon5, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 6 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon6 = true;
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Weapon6, transform.position, Quaternion.identity);
                ran = true;
            }
            //Ammo
            if (Weapon1 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon2 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo2, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo2, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon3 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon4 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo3, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo3, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon5 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo2, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo2, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon6 && GameObject.Find("_GameManager").GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(GameObject.Find("_GameManager").GetComponent<GameManagerScript>().Ammo1, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
        }
	}
}
