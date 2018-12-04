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
    GameObject Inventory;
    [SerializeField]
    GameObject Options;
    [SerializeField]
    GameObject Disconnect;
    [SerializeField]

    public RectTransform inventoryPanel;

    public Vector3 panelHiddenPosition = new Vector3(0, -9999, 0);


    private NetworkManager networkManager;
    private NetworkDiscoveryScript networkDiscoveryScript;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();
        inventoryPanel.transform.position = panelHiddenPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn == false)
        {
            inventoryPanel.position = panelHiddenPosition;
            inventoryPanel.anchorMax = new Vector2(1, 0);
            inventoryPanel.anchorMin = new Vector2(0, -1);

        }
    }

    public void ShowResume()
    {
        Resume.SetActive(true);
        Inventory.SetActive(false);
        Options.SetActive(false);
        Disconnect.SetActive(false);
        inventoryPanel.position = panelHiddenPosition;
        inventoryPanel.anchorMax = new Vector2(1, 0);
        inventoryPanel.anchorMin = new Vector2(0, -1);

    }

    public void ShowInventory()
    {
        Resume.SetActive(false);
        Inventory.SetActive(true);
        Options.SetActive(false);
        Disconnect.SetActive(false);
        inventoryPanel.position = Vector3.zero;
        inventoryPanel.anchorMax = new Vector2(1, 1);
        inventoryPanel.anchorMin = new Vector2(0, 0);
    }

    public void ShowOptions()
    {
        Resume.SetActive(false);
        Inventory.SetActive(false);
        Options.SetActive(true);
        Disconnect.SetActive(false);
        inventoryPanel.position = panelHiddenPosition;
        inventoryPanel.anchorMax = new Vector2(1, 0);
        inventoryPanel.anchorMin = new Vector2(0, -1);
    }

    public void ShowDisconnect()
    {
        Resume.SetActive(false);
        Inventory.SetActive(false);
        Options.SetActive(false);
        Disconnect.SetActive(true);
        inventoryPanel.position = panelHiddenPosition;
        inventoryPanel.anchorMax = new Vector2(1, 0);
        inventoryPanel.anchorMin = new Vector2(0, -1);
    }

    public void LeaveRoom()
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
