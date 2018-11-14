using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour {

    [SerializeField]
    bool LANOnly = false;

    [SerializeField]
    GameObject OnlineUI;
    [SerializeField]
    GameObject LANUI;
    [SerializeField]
    GameObject OnlineButton;
    [SerializeField]
    Text statusLANText;
    [SerializeField]
    Text createLANStatusText;

    private NetworkManager networkManager;
    private NetworkDiscoveryScript networkDiscoveryScript;
    private HostGame hostGameScript;

    // Use this for initialization
    void Start () {

        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();
        networkDiscoveryScript.Initialize();
        if (networkDiscoveryScript.isClient)
            networkDiscoveryScript.StopBroadcast();
        NetworkDiscoveryScript.isInLAN = false;
        networkDiscoveryScript.statusText = statusLANText;
        statusLANText.text = "";
        networkDiscoveryScript.createStatusText = createLANStatusText;
        createLANStatusText.text = "";
        hostGameScript = networkManager.GetComponent<HostGame>();

        if (LANOnly)
        {
            OnlineUI.SetActive(false);
            LANUI.SetActive(true);
            OnlineButton.SetActive(false);
        }
        else {
            OnlineUI.SetActive(true);
            LANUI.SetActive(false);
        }
	}

    public void ChangeToLAN()
    {
        networkManager.StopMatchMaker();
        OnlineUI.SetActive(false);
        LANUI.SetActive(true);
    }

    public void ChangeToOnline()
    {
        networkManager.StartMatchMaker();
        OnlineUI.SetActive(true);
        LANUI.SetActive(false);
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
    public void SetRoomName(string _name)
    {
        hostGameScript.SetRoomName(_name);
    }
    public void CreateRoom()
    {
        hostGameScript.CreateRoom();
    }

}
