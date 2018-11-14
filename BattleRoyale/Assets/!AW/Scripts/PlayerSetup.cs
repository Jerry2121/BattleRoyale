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
    GameObject playerGraphics;
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
            AssignRemoteLayer();
        }
        else
        {
            //Disable player graphics for local player
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));

            //Create player UI
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

            //Canfigure playerUI
            PlayerUI ui = playerUIInstance.GetComponent<PlayerUI>();
            if(ui == null)
            {
                Debug.LogError("PlayerSetup -- Start: No PlayerUI on PlayerUI prefab");
                return;
            }
            ui.SetPlayerController(GetComponent<PlayerController>());

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
            GameManager.instance.SetSceneCameraActiveState(true);

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
