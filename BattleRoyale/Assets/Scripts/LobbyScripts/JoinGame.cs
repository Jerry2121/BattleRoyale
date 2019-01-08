using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using TMPro;

public class JoinGame : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI onlineStatusText;

    [SerializeField]
    private GameObject roomListItemPrefab;

    [SerializeField]
    private Transform roomListParent;

    List<GameObject> roomList = new List<GameObject>();

    private NetworkManager networkManager;

	// Use this for initialization
	void Start () {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }

        RefreshRoomList();
	}

    public void RefreshRoomList()
    {
        ClearRoomList();

        if (networkManager.matchMaker == null)
            networkManager.StartMatchMaker();

        networkManager.matchMaker.ListMatches(0, 20, "", false, 0, 0, OnMatchList);
        onlineStatusText.text = "Loading...";
    }

    public void OnMatchList(bool _success, string _extendedInfo, List<MatchInfoSnapshot> _matches)
    {
        onlineStatusText.text = "";

        if (_success == false || _matches == null)
        {
            onlineStatusText.text = "Couldn't get room list";
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
            onlineStatusText.text = "No rooms at the moment";
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
        Debug.Log("Joining " + _match.name);

        networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        StartCoroutine(WaitForJoin());
        Debug.Log("After Corout");
    }

    IEnumerator WaitForJoin()
    {
        Debug.Log("WaitForEndOfFrame join start");
        roomList.Clear();

        int countdown = 10;
        while (countdown > 0)
        {
            onlineStatusText.text = "Joining... (" + countdown + ")";
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        Debug.Log("WaitForEndOfFrame join after count");

        //We failed to connect
        onlineStatusText.text = "Failed to connect";
        yield return new WaitForSeconds(1f);

        MatchInfo matchInfo = networkManager.matchInfo;
        if (matchInfo != null)
        {
            networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
            networkManager.StopHost();
        }
        RefreshRoomList();
        Debug.Log("WaitForEndOfFrame join end");
    }

}
