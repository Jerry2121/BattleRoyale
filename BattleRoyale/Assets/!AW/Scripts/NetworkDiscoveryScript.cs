using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkDiscoveryScript : NetworkDiscovery {

    public static bool isInLAN;

    private NetworkManager networkManager;
    private NetworkDiscovery networkDiscovery;

    [HideInInspector]
    public Text statusText;
    [HideInInspector]
    public Text createStatusText;

	// Use this for initialization
	void Start () {
        networkManager = GetComponent<NetworkManager>();
        Initialize();
    }

    public void JoinLANGame()
    {
        StartAsClient();
        isInLAN = true;
        StartCoroutine(WaitForJoin());
    }

    IEnumerator WaitForJoin()
    {
        int countdown = 10;
        while (countdown > 0)
        {
            if(statusText != null)
                statusText.text = "Joining Local Game... (" + countdown + ")";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        //We failed to connect
        if(statusText != null)
        statusText.text = "Failed to connect to a local game";
        StopBroadcast();
        yield return new WaitForSeconds(1f);
        statusText.text = "";
        isInLAN = false;
        Initialize();
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        networkManager.networkAddress = fromAddress;
        //See if it is already connected, or else it will try to start and make a new player multipule times
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
        StartCoroutine(WaitForCreate());
    }
    public void CreateLANGameAsServer()
    {
        networkManager.StartServer();
        StartAsServer();
        isInLAN = true;
        StartCoroutine(WaitForCreate());
    }

    IEnumerator WaitForCreate()
    {
        int countdown = 5;
        while (countdown > 0)
        {
            if(createStatusText != null)
            createStatusText.text = "Creating Local Game...";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        //We failed to create a game, likely because a local game is already running on the network
        if(createStatusText != null)
            createStatusText.text = "Failed to create a local game. Make sure there are no other local games running on the network";
        StopBroadcast();
        yield return new WaitForSeconds(1f);
        isInLAN = false;
        Initialize();
    }

}
