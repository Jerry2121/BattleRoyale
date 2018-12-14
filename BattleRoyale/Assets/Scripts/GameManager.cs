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

    public int timeBetweenZoneShrinking = 120;
    public bool inStartPeriod = true;
    public float gameTimer = 0f;
    float zoneTimer = 0f;
    float zoneWaitTimer = 0f;
    [SerializeField]
    GameObject zoneWallPrefab;
    [SerializeField]
    Transform zoneWallTransform;
    [SerializeField]
    GameObject sceneCamera;
    [SerializeField]
    GameObject airDropPrefab;
    [SerializeField]
    Transform[] airDropSpawnPoints;

    float airdroptimer = 20;

    public bool zoneShrinking;
    public bool zoneShrunk;
    bool waitTimerActive;

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
        zoneShrinking = true;
        zoneTimer = 0;

        //For choosing a random position to close on
        /*int x = Random.Range(0, 1);
        int z = Random.Range(0, 1);
        zoneWallTransform.position += new Vector3(x, 0f, y);
        */
        SpawnAirdrop();
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
        gameTimer += Time.deltaTime;

        if (gameTimer < 0f)
        {
            inStartPeriod = true;
        }
        else
        {
            inStartPeriod = false;
        }

        if (zoneShrunk)
            return;

        if(zoneTimer > 0)
        {
            zoneShrinking = true;
            zoneTimer -= Time.deltaTime;
        }
        else
        {
            zoneTimer = 0f;
            zoneShrinking = false;
        }
        if(zoneShrinking == false)
        {
            if(waitTimerActive == false)
            {
                zoneWaitTimer = timeBetweenZoneShrinking;
                waitTimerActive = true;
            }

            zoneWaitTimer -= Time.deltaTime;

            if(zoneWaitTimer <= 0)
            {
                zoneTimer = 60;
                zoneShrinking = true;
                waitTimerActive = false;
            }

        }
        

        //if (zoneShrinking)
        //    zoneWallTransform.localScale -= new Vector3(0.005f * Time.deltaTime, 0f, 0.005f * Time.deltaTime);

    }

    void StartTimer()
    {
        gameTimer = -matchSettings.startTime;
    }

    void SpawnAirdrop()
    {
        if(networkDiscoveryScript.isServer)
            Utility.InstantiateOverNetwork(airDropPrefab, airDropSpawnPoints[Random.Range(0, airDropSpawnPoints.Length)].position, Quaternion.identity);
        
    }

    /*void SpawnZoneWall()
    {
        networkManager = NetworkManager.singleton;
        networkDiscoveryScript = networkManager.GetComponent<NetworkDiscoveryScript>();

        if (networkDiscoveryScript.isServer)
        {
            GameObject zone_GO = Utility.InstantiateOverNetwork(zoneWallPrefab, transform.position, Quaternion.identity);
            zoneWallTransform = zone_GO.transform;
        }
    }*/

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
    public static Player GetLocalPlayer()
    {
        foreach(Player player in players.Values)
        {
            if (player.isLocalPlayer)
            {
                return player;
            }
        }
        return null;
    }

    #endregion
}
