using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyButtonConnection : MonoBehaviour {

    private NetworkManager networkManager;
    private NetworkDiscoveryScript networkDiscoveryScript;
    private HostGame hostGameScript;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();
        networkDiscoveryScript.Initialize();
        if(networkDiscoveryScript.isClient)
            networkDiscoveryScript.StopBroadcast();
        NetworkDiscoveryScript.isInLAN = false;
        hostGameScript = networkManager.GetComponent<HostGame>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void JoinLANGame()
    {
        networkDiscoveryScript.JoinLANGame();
    }

    public void CreateLANGameAsHost()
    {
        networkDiscoveryScript.CreateLANGameAsHost();
    }
    public void CreateLANGameAsServer()
    {
        networkDiscoveryScript.CreateLANGameAsServer();
    }
    public void CreateRoom()
    {
        hostGameScript.CreateRoom();
    }
}
