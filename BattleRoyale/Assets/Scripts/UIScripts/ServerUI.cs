using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ServerUI : NetworkBehaviour {

    [SerializeField]
    GameObject serverScoreboard;

    private NetworkManager networkManager;
    private NetworkDiscoveryScript networkDiscoveryScript;

    [SerializeField]
    TextMeshProUGUI gameTimer;

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
        StartCoroutine(ActivateCamera());

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator ActivateCamera()
    {
        Debug.Log("ServerUI activate cam");
        yield return new WaitForSeconds(0.5f);
        if (GameManager.Instance == null)
            yield return new WaitForSeconds(5f);
        GameManager.Instance.SetSceneCameraActiveState(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (NetworkDiscoveryScript.IsServerOnly == false)
            return;

        if(Cursor.lockState == CursorLockMode.Locked && spectCam == null || Cursor.lockState == CursorLockMode.Locked && spectCam.activeSelf == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            serverScoreboard.SetActive(!serverScoreboard.activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleSpectatorCam();
        }

        try
        {
            float seconds = GameManager.Instance.gameTimer;
            TimeSpan time = TimeSpan.FromSeconds(seconds);

            gameTimer.text = (time.ToString(@"mm\:ss"));
        }
        catch (NullReferenceException ex)
        {
            //Do nothing, this is expected to happen
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
            spectCam = Instantiate(spectCamPrefab, new Vector3(0f, 200f, 0f), Quaternion.identity);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameManager.Instance.SetSceneCameraActiveState(false);
            return;
        }

        if (spectCam.activeSelf == false)
        {
            spectCam.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameManager.Instance.SetSceneCameraActiveState(false);
        }
        else
        {
            spectCam.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameManager.Instance.SetSceneCameraActiveState(true);
        }


    }

}
