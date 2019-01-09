using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerUI : NetworkBehaviour {

    [SerializeField]
    GameObject serverScoreboard;

    private NetworkManager networkManager;
    private NetworkDiscoveryScript networkDiscoveryScript;

    [SerializeField]
    GameObject spectCamPrefab;
    GameObject spectCam;

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
        if (NetworkDiscoveryScript.IsServerOnly == false)
            return;

        if(Cursor.lockState == CursorLockMode.Locked && spectCam.activeSelf == false)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            serverScoreboard.SetActive(!serverScoreboard.activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleSpectatorCam();
        }
	}

    public void StopServer()
    {
        networkManager.StopServer();
        NetworkDiscoveryScript.IsInLAN = false;
        NetworkDiscoveryScript.IsServerOnly = false;
        networkDiscoveryScript.StopBroadcast();
    }

    void ToggleSpectatorCam()
    {
        if (spectCam == null)
        {
            spectCam = Instantiate(spectCamPrefab, Vector3.zero, Quaternion.identity);
            GameManager.instance.SetSceneCameraActiveState(false);
            return;
        }

        if (spectCam.activeSelf == false)
        {
            spectCam.SetActive(true);
            GameManager.instance.SetSceneCameraActiveState(false);
        }
        else
        {
            spectCam.SetActive(false);
            GameManager.instance.SetSceneCameraActiveState(true);
        }


    }

}
