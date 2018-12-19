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
            GameObject gameManager = GameManager.instance.gameObject;

            if (d1 == 1 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon1 = true;
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Weapon1, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 2 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon2 = true;
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Weapon2, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 3 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon3 = true;
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Weapon3, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 4 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon4 = true;
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Weapon4, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 5 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon5 = true;
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Weapon5, transform.position, Quaternion.identity);
                ran = true;
            }
            if (d1 == 6 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && canspawnItem && !ran)
            {
                Weapon6 = true;
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Weapon6, transform.position, Quaternion.identity);
                ran = true;
            }
            //Ammo
            if (Weapon1 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo1, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo1, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon2 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo2, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo2, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon3 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo1, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo1, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon4 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo3, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo3, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon5 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo2, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo2, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
            if (Weapon6 && gameManager.GetComponent<GameManagerScript>().DisableItemSpawning == false && ran)
            {
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo1, AmmoSpawner1.transform.position, Quaternion.identity);
                Utility.InstantiateOverNetwork(gameManager.GetComponent<GameManagerScript>().Ammo1, AmmoSpawner2.transform.position, Quaternion.identity);
                canspawnItem = false;
            }
        }
	}
}
