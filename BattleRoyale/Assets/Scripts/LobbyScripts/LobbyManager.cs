using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager : MonoBehaviour {

    [SerializeField]
    bool LANOnly = false;

    [SerializeField]
    GameObject LobbyUICanvas;
    [SerializeField]
    GameObject MainMainMenuCanvas;

    [SerializeField]
    GameObject OnlineUI;
    [SerializeField]
    GameObject LANUI;
    [SerializeField]
    GameObject OnlineButton;
    [SerializeField]
    TextMeshProUGUI statusLANText;
    [SerializeField]
    TextMeshProUGUI createLANStatusText;
    [SerializeField]
    GameObject newsButton;

    private NetworkManager networkManager;
    private NetworkDiscoveryScript networkDiscoveryScript;
    private HostGame hostGameScript;
    string lobbySceneName;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LobbyUICanvas.SetActive(false);
        MainMainMenuCanvas.SetActive(true);
        lobbySceneName = SceneManager.GetActiveScene().name;
        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();
        networkDiscoveryScript.Initialize();
        if (networkDiscoveryScript.isClient)
            networkDiscoveryScript.StopBroadcast();

        NetworkDiscoveryScript.IsInLAN = false;
        networkDiscoveryScript.lobbyManager = this;

        statusLANText.text = "";
        createLANStatusText.text = "";
        hostGameScript = networkManager.GetComponent<HostGame>();

        ChangeToLAN();

#if  DEVELOPMENT_BUILD == false && UNITY_EDITOR == false
        LANOnly = false;
#endif

        if (LANOnly)
        {
            OnlineButton.SetActive(false);
        }

    }


    public void GoToLobby()
    {
        MainMainMenuCanvas.SetActive(false);
        LobbyUICanvas.SetActive(true);
        newsButton.SetActive(false);
    }
    public void GoToMainMenu()
    {
        MainMainMenuCanvas.SetActive(true);
        LobbyUICanvas.SetActive(false);
        newsButton.SetActive(true);
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
        Debug.Log("Lobby:CreateLANGameAsHost");
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().volume = 0;
        networkDiscoveryScript.CreateLANGameAsHost();
    }
    public void CreateLANGameAsServer()
    {
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().volume = 0;
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

    public IEnumerator WaitForJoinLAN()
    {
        int countdown = 10;
        while (countdown > 0)
        {
            if (statusLANText != null)
                statusLANText.text = "Joining Local Game... (" + countdown + ")";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        //if we have changed scenes
        if (SceneManager.GetActiveScene().name != lobbySceneName)
            yield break;

        //We failed to connect
        if (statusLANText != null)
            statusLANText.text = "Failed to connect to a local game";
        networkDiscoveryScript.StopBroadcast();
        yield return new WaitForSeconds(1f);
        statusLANText.text = "";
        NetworkDiscoveryScript.IsInLAN = false;
        networkDiscoveryScript.Initialize();
    }

    public IEnumerator WaitForCreateLAN()
    {
        Debug.Log("WaitForCreateLAN");
        int countdown = 15;
        while (countdown > 0)
        {
            if (createLANStatusText != null)
                createLANStatusText.text = "Creating Local Game...";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        Debug.Log("WaitForCreateLAN:AfterCount");
        //if we have changed scenes
        if (SceneManager.GetActiveScene().name != lobbySceneName)
            yield break;
        Debug.Log("WaitForCreateLAN:SceneNotChanged");
        
        //We failed to create a game, likely because a local game is already running on the network
        if (createLANStatusText != null)
            createLANStatusText.text = "Failed to create a local game. Make sure there are no other local games running on the network";
        networkDiscoveryScript.StopBroadcast();
        yield return new WaitForSeconds(1f);
        NetworkDiscoveryScript.IsInLAN = false;
        networkDiscoveryScript.Initialize();

    }

}
