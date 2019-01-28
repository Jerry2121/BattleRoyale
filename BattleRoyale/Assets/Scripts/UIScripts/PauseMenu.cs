using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PauseMenu : MonoBehaviour {

    public static bool isOn;
    
    [SerializeField]
    GameObject Resume;
    [SerializeField]
    GameObject Options;
    [SerializeField]
    GameObject Disconnect;

    private NetworkManager networkManager;
    private NetworkDiscoveryScript networkDiscoveryScript;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();
    }

    public void ShowResume()
    {
        Resume.SetActive(true);
        Options.SetActive(false);
        Disconnect.SetActive(false);
    }

    public void ShowInventory()
    {
        Resume.SetActive(false);
        Options.SetActive(false);
        Disconnect.SetActive(false);    }

    public void ShowOptions()
    {
        Resume.SetActive(false);
        Options.SetActive(true);
        Disconnect.SetActive(false);
    }

    public void ShowDisconnect()
    {
        Resume.SetActive(false);
        Options.SetActive(false);
        Disconnect.SetActive(true);
    }

    public void LeaveGame()
    {
        if (NetworkDiscoveryScript.IsInLAN)
        {
            networkManager.StopHost();
            NetworkDiscoveryScript.IsInLAN = false;
            networkDiscoveryScript.StopBroadcast();
        }
        else
        {
            MatchInfo matchInfo = networkManager.matchInfo;
            networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
            networkManager.StopHost();
        }
    }

}
