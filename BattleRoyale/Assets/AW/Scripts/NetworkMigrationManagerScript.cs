using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkMigrationManagerScript : NetworkMigrationManager {

    [SerializeField]
    Canvas networkMigrationCanvas;

    private void Awake()
    {
        Initialize(NetworkManager.singleton.client, NetworkManager.singleton.matchInfo);
        networkMigrationCanvas.enabled = false;
    }

    public void FindNewHostButton()
    {
        PeerInfoMessage newHostInfo;
        bool youAreNewHost;
        bool findNewHost = FindNewHost(out newHostInfo, out youAreNewHost);
        if(findNewHost)
            networkMigrationCanvas.enabled = false;

    }

    protected override void OnClientDisconnectedFromHost(NetworkConnection conn, out SceneChangeOption sceneChange)
    {
        networkMigrationCanvas.enabled = true;
        sceneChange = SceneChangeOption.StayInOnlineScene;
    }

    public override bool FindNewHost(out PeerInfoMessage newHostInfo, out bool youAreNewHost)
    {
        return base.FindNewHost(out newHostInfo, out youAreNewHost);
    }

}
