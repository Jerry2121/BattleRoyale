using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnZoneCircle : MonoBehaviour {

    public GameObject ZoneWallPrefab;

    NetworkManager networkManager;
    NetworkDiscoveryScript networkDiscoveryScript;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();

        if (networkDiscoveryScript.isServer)
        {
            Utility.InstantiateOverNetwork(ZoneWallPrefab, transform.position, Quaternion.identity);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
