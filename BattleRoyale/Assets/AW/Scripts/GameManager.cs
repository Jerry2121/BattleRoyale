using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public static NetworkManager networkManager;
    public static NetworkDiscoveryScript networkDiscoveryScript;

    public MatchSettings matchSettings;

    public bool inStartPeriod = true;
    public float timer = 0f;

    [SerializeField]
    GameObject sceneCamera;

    public delegate void OnPlayerKilledCallback(string player, string source);
    public OnPlayerKilledCallback onPlayerKilledCallback;

    private void Awake()
    {
        if(instance != null)
        {
            if (Debug.isDebugBuild)
                Debug.LogWarning("GameManager -- Awake: There is more than one GameManager in the scene. Only one will be set to GameManager.instance.");
            return;
        }
        instance = this;
        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();
        StartTimer();
    }

    public void SetSceneCameraActiveState(bool isActive)
    {
        if(sceneCamera == null)
        {
            if(Debug.isDebugBuild)
                Debug.LogError("GameManager -- SetSceneCameraActiveState: The sceneCamera is null!");
            return;
        }

        sceneCamera.SetActive(isActive);
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            inStartPeriod = true;
        }
        else
        {
            inStartPeriod = false;
            timer = 0f;
        }
    }

    void StartTimer()
    {
        timer = matchSettings.startTime;
    }

    #region Player Tracking

    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string _netID, Player _player)
    {
        string playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(playerID, _player);
        _player.transform.name = playerID;
    }

    public static void UnregisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    public static Player GetPlayer(string _playerID)
    {
        if (players.ContainsKey(_playerID))
            return players[_playerID];
        else return null;
    }

    public static Player[] GetAllPlayers()
    {
        return players.Values.ToArray();
    }

    /*private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach(string playerID in players.Keys)
        {
            GUILayout.Label(playerID + "  -  " + players[playerID].transform.name);
        }

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }*/
    #endregion
}
