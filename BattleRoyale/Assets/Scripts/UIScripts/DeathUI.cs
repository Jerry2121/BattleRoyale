using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class DeathUI : MonoBehaviour {

    public GameObject deathUIMap;
    public GameObject deathScoreboard;

    // Use this for initialization
    void Start () {
        if (NetworkDiscoveryScript.IsServerOnly)
            gameObject.SetActive(false);

        if (GameManager.Instance.matchSettings.canRespawn)
            deathUIMap.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleScoreboard();
        }
    }

    public void LeaveRoom()
    {
        if (NetworkDiscoveryScript.IsInLAN)
        {
            GameManager.networkManager.StopHost();
            NetworkDiscoveryScript.IsInLAN = false;
            GameManager.networkDiscoveryScript.StopBroadcast();
        }
        else
        {
            MatchInfo matchInfo = GameManager.networkManager.matchInfo;
            GameManager.networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, GameManager.networkManager.OnDropConnection);
            GameManager.networkManager.StopHost();
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void ToggleScoreboard()
    {
        deathScoreboard.SetActive(!deathScoreboard.activeSelf);
    }

}
