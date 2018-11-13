using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

    [SerializeField]
    private uint roomSize = 20;

    private string roomName;

    private NetworkManager networkManager;
    //[SerializeField]
    //private NetworkDiscovery networkDiscovery;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;
        //networkDiscovery.Initialize();
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetRoomName(string _name)
    {
        roomName = _name;
    }

    public void CreateRoom()
    {
        if(roomName != "" && roomName != null)
        {
            if (Debug.isDebugBuild)
                Debug.Log("Creating room " + roomName + " that can have " + roomSize + " players");

            //Create the room
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
        }
    }

    /*public void CreateLANGameAsHost()
    {
        networkManager.StartHost();
        networkDiscovery.StartAsServer();
    }
    public void CreateLANGameAsServer()
    {
        networkManager.StartServer();
        networkDiscovery.StartAsServer();
    }*/
}
