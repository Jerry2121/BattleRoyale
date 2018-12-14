using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemSpawn : NetworkBehaviour {

    public GameObject itemPrefab;

    NetworkManager networkManager;
    NetworkDiscoveryScript networkDiscoveryScript;

    // Use this for initialization
    void Start () {
        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();

        //Only Instantiate objects on the server, then tell the server to spawn it on all clients
        if (networkDiscoveryScript.isServer)
        {
            if(itemPrefab != null)
                Utility.InstantiateOverNetwork(itemPrefab, this.transform.position, Quaternion.identity);
        }

    }
}
