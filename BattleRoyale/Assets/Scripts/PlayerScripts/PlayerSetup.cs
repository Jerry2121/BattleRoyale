using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerController))]
public class PlayerSetup : NetworkBehaviour {

    //components to disable on non-local players
    [SerializeField]
    Behaviour[] componentsToDisable;
    [SerializeField]
    string remoteLayerName = "RemotePlayer";
    [SerializeField]
    string dontDrawLayerName = "DontDraw";
    [SerializeField]
    string remoteMinimapLayerName = "MiniMapRemote";
    [SerializeField]
    GameObject playerGraphics;
    [SerializeField]
    GameObject playerMiniMapGraphic;
    [SerializeField]
    GameObject playerUIPrefab;
    [HideInInspector]
    public GameObject playerUIInstance;

	// Use this for initialization
	void Start () {
        //if the object isn't controlled by the local player
        if (!isLocalPlayer)
        {
            DisableComponents();
            playerMiniMapGraphic.layer = LayerMask.NameToLayer(remoteMinimapLayerName);
            playerMiniMapGraphic.transform.localScale = new Vector3(2f,2f,2f);
            AssignRemoteLayer();
        }
        else
        {
            //Disable player graphics for local player
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

            //Create player UI
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            //Configure playerUI
            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
            if(ui == null)
            {
                Debug.LogError("PlayerSetup -- Start: No PlayerUI on PlayerUI prefab");
                return;
            }
            ui.SetPlayer(GetComponent<Player>());

            GetComponent<Player>().SetupPlayer();

            string username = "Loading...";
            if (UserAccountManager.IsLoggedIn)
                username = UserAccountManager.PlayerUsername;
            else username = transform.name;

            CmdSetUsername(transform.name, username);
        }
	}

    [Command]
    void CmdSetUsername(string _playerID, string _username)
    {
        Player player = GameManager.GetPlayer(_playerID);
        if(player != null)
        {
            player.username = _username;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        if (Debug.isDebugBuild)
            Debug.Log("PlayerSetup -- OnStartClient");

        GameManager.RegisterPlayer(netID, player);
    }

    public override void OnStartServer()
    {
        //If we are the host OnStartClient and OnStartServer are oth called, which causes errors, so we'll only run the code in OnStartClient
        if (NetworkDiscoveryScript.IsServerOnly == false)
            return;

        base.OnStartServer();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        if (Debug.isDebugBuild)
            Debug.Log("PlayerSetup -- OnStartServer");

        GameManager.RegisterPlayer(netID, player);
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    void OnDisable()
    {
        Destroy(playerUIInstance);

        if(isLocalPlayer)
            GameManager.Instance.SetSceneCameraActiveState(true);

        GameManager.UnregisterPlayer(transform.name);

    }

    void SetLayerRecursively(GameObject _obj, int _newLayer)
    {
        _obj.layer = _newLayer;

        foreach(Transform child in _obj.transform)
        {
            SetLayerRecursively(child.gameObject, _newLayer);
        }
    }

}
