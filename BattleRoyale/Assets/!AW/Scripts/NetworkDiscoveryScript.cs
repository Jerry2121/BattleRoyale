using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkDiscoveryScript : NetworkDiscovery {

    public static bool isInLAN;

    private NetworkManager networkManager;
    private NetworkDiscovery networkDiscovery;

	// Use this for initialization
	void Start () {
        networkManager = GetComponent<NetworkManager>();
        Initialize();
    }

    public void JoinLANGame()
    {
        StartAsClient();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        networkManager.networkAddress = fromAddress;
        //Keep it from trying to start and make a new player multipule times
        if (networkManager.IsClientConnected() == false)
        {
            networkManager.StartClient();
            isInLAN = true;
        }
    }

    public void CreateLANGameAsHost()
    {
        networkManager.StartHost();
        StartAsServer();
        isInLAN = true;
    }
    public void CreateLANGameAsServer()
    {
        networkManager.StartServer();
        StartAsServer();
        isInLAN = true;
    }

}
