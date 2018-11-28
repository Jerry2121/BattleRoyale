using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerUI : NetworkBehaviour {

    private NetworkManager networkManager;
    private NetworkDiscoveryScript networkDiscoveryScript;

    // Use this for initialization
    void Start () {

        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();

        if (networkDiscoveryScript.isServer == false)
            gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StopServer()
    {
        networkManager.StopHost();
        NetworkDiscoveryScript.IsInLAN = false;
        networkDiscoveryScript.StopBroadcast();
    }

}
