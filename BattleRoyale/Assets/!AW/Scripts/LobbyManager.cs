using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class LobbyManager : MonoBehaviour {

    [SerializeField]
    bool LANOnly = false;

    [SerializeField]
    GameObject OnlineUI;
    [SerializeField]
    GameObject LANUI;
    [SerializeField]
    GameObject OnlineButton;

    private NetworkManager networkManager;

	// Use this for initialization
	void Start () {

        networkManager = NetworkManager.singleton;

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

}
