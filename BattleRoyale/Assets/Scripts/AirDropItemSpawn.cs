using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AirDropItemSpawn : NetworkBehaviour
{
    //[SerializeField]
    GameObject[] weapons;
    //[SerializeField]
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
        weapons = GameManager.Instance.gameManagerScript.weapons;
        items = GameManager.Instance.gameManagerScript.items;
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
        if (networkDiscoveryScript.isServer)
        {
            for (int i = 0; i < weaponsToSpawn; i++)
            {
                int chance = Random.Range(0, weapons.Length);

                Utility.InstantiateOverNetwork(weapons[chance], itemWeaponSpawnPoints[Random.Range(0, itemWeaponSpawnPoints.Length)].position, Quaternion.identity);
            }
        }
    }

    void SpawnItems()
    {
        if (networkDiscoveryScript.isServer)
        {
            for (int i = 0; i < itemsToSpawn; i++)
            {
                int chance = Random.Range(0, items.Length);

                Utility.InstantiateOverNetwork(items[chance], itemWeaponSpawnPoints[Random.Range(0, itemWeaponSpawnPoints.Length)].position, Quaternion.identity);
            }
        }
    }

}
