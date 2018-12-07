using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerUI : NetworkBehaviour {

    [SerializeField]
    GameObject serverScoreboard;

    private NetworkManager networkManager;
    private NetworkDiscoveryScript networkDiscoveryScript;

    // Use this for initialization
    void Start() {

        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();

        if (NetworkDiscoveryScript.IsServerOnly == false) //We're not running in server-only mode,  so turn off the UI
        {
            gameObject.SetActive(false);
            return;
        }
        GameManager.instance.SetSceneCameraActiveState(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            serverScoreboard.SetActive(!serverScoreboard.activeSelf);
        }
	}

    public void StopServer()
    {
        networkManager.StopServer();
        NetworkDiscoveryScript.IsInLAN = false;
        NetworkDiscoveryScript.IsServerOnly = false;
        networkDiscoveryScript.StopBroadcast();
    }

}
