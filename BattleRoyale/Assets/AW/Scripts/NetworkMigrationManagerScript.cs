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
