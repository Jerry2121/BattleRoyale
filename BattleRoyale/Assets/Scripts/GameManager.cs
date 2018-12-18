﻿using System.Collections;
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
    [SerializeField]
    Transform[] MapSpawns;

    float airdroptimer = 20;
    float secondsUntilDrop;

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
        if (!inStartPeriod)
        {
            if (networkDiscoveryScript.isServer)
            {
                Player[] ply = GetAllPlayers();
                for (int i = 0; i < ply.Length; i++)
                {
                    int chance = Random.Range(0, MapSpawns.Length - 1);
                    if (MapSpawns[chance].GetComponent<MapSpawn>().Occupied == false)
                    {
                        ply[i].transform.position = MapSpawns[chance].transform.position;
                        MapSpawns[chance].GetComponent<MapSpawn>().Occupied = true;
                    }
                    else
                    {
                        i--;
                    }
                }
            }
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
        
        if(gameTimer >= -420)
        {
            secondsUntilDrop -= Time.deltaTime;

            if(secondsUntilDrop <= 0)
            {
                Debug.Log("Foo");
                SpawnAirdrop();
                secondsUntilDrop = Random.Range(5, 36);
            }
        }

    }

    void StartTimer()
    {
        gameTimer = -matchSettings.startTime;
    }

    void SpawnAirdrop()
    {
        Debug.Log("Foo2");
        if (networkDiscoveryScript.isServer)
        {
            if(airDropSpawnPoints.Length < 1)
            {
                Debug.LogError("GameManager -- SpawnAirdrop: There are no Airdrop Spawnpoints");
            }
            Utility.InstantiateOverNetwork(airDropPrefab, airDropSpawnPoints[Random.Range(0, airDropSpawnPoints.Length)].position, Quaternion.identity);
        }
        
    }

    public static bool IsGameOver()
    {
        if (players.Count > 1 || instance.inStartPeriod)
            return false;
        else
            return true;
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
