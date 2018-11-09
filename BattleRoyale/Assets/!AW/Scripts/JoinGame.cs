using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinGame : MonoBehaviour {

    [SerializeField]
    private Text statusText;

    [SerializeField]
    private GameObject roomListItemPrefab;

    [SerializeField]
    private Transform roomListParent;

    List<GameObject> roomList = new List<GameObject>();

    private NetworkManager networkManager;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;
        if(networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }

        RefreshRoomList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RefreshRoomList()
    {
        ClearRoomList();
        networkManager.matchMaker.ListMatches(0, 20, "", false, 0, 0, OnMatchList);
        statusText.text = "Loading...";
    }

    public void OnMatchList(bool _success, string _extendedInfo, List<MatchInfoSnapshot> _matches)
    {
        statusText.text = "";

        if (_matches == null)
        {
            statusText.text = "Couldn't get room list";
            return;
        }

        foreach (MatchInfoSnapshot match in _matches)
        {
            GameObject roomListItemGameObject = SimplePool.Spawn(roomListItemPrefab, new Vector3(0,0,0), Quaternion.identity);
            roomListItemGameObject.transform.SetParent(roomListParent);

            //RoomListItem handels syncing the button to the match
            RoomListItem roomListItem = roomListItemGameObject.GetComponent<RoomListItem>();
            if(roomListItem != null)
            {
                roomListItem.Setup(match, JoinRoom);
            }



            roomList.Add(roomListItemGameObject);
        }

        if(roomList.Count == 0)
        {
            statusText.text = "No rooms at the moment";
        }

    }

    void ClearRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot _match)
    {
        if (Debug.isDebugBuild)
            Debug.Log("Joining " + _match.name);

        networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        roomList.Clear();
        statusText.text = "Joining";
    }

}
