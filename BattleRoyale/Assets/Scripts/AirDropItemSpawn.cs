using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AirDropItemSpawn : NetworkBehaviour {

    [SerializeField]
    GameObject[] weapons;
    [SerializeField]
    GameObject[] items;
    [SerializeField]
    Transform[] itemWeaponSpawnPoints;

    [SerializeField]
    int weaponsToSpawn;
    [SerializeField]
    int itemsToSpawn;


    NetworkManager networkManager;
    NetworkDiscoveryScript networkDiscoveryScript;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();
    }

    public void SpawnSupplies()
    {
        SpawnWeapons();
        SpawnItems();
        Destroy(gameObject);
    }

    void SpawnWeapons()
    {
        for (int i = 0; i < weaponsToSpawn; i++)
        {
            int chance = Random.Range(0, weapons.Length);

            if (networkDiscoveryScript.isServer)
            {
                Utility.InstantiateOverNetwork(weapons[chance], itemWeaponSpawnPoints[Random.Range(0, itemWeaponSpawnPoints.Length)].position, Quaternion.identity);
            }

        }

    }

    void SpawnItems()
    {
        for (int i = 0; i < itemsToSpawn; i++)
        {
            int chance = Random.Range(0, items.Length);

            if (networkDiscoveryScript.isServer)
            {
                Utility.InstantiateOverNetwork(items[chance], itemWeaponSpawnPoints[Random.Range(0, itemWeaponSpawnPoints.Length)].position, Quaternion.identity);
            }

        }
    }

}
