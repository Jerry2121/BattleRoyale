using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;

public class NetworkMigrationManagerScript : NetworkMigrationManager {

    [SerializeField]
    Canvas networkMigrationCanvas;
    NetworkManager networkManager = NetworkManager.singleton;
    NetworkDiscoveryScript networkDiscoveryScript;

    private void Awake()
    {
        networkMigrationCanvas.enabled = false;
    }

    public void FindNewHostButton()
    {
        PeerInfoMessage newHostInfo;
        bool youAreNewHost;
        bool findNewHost = FindNewHost(out newHostInfo, out youAreNewHost);
        if(findNewHost)
            ToggleCanvas();
    }
    public void LeaveGame()
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

    protected override void OnClientDisconnectedFromHost(NetworkConnection conn, out SceneChangeOption sceneChange)
    {
        ToggleCanvas();
        sceneChange = SceneChangeOption.StayInOnlineScene;
    }

    public override bool FindNewHost(out PeerInfoMessage newHostInfo, out bool youAreNewHost)
    {
        return base.FindNewHost(out newHostInfo, out youAreNewHost);
    }
    
    private void ToggleCanvas()
    {
        if (networkMigrationCanvas.enabled)
        {
            networkMigrationCanvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        networkMigrationCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
