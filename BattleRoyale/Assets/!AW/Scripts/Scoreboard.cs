using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour {

    [SerializeField]
    GameObject playerScoreboardItemPrefab;
    [SerializeField]
    Transform playerScoreboardList;
    
	void OnEnable () {
        //Get an array of players
        Player[] players = GameManager.GetAllPlayers();

        //Loop through and set up item for each
        foreach (Player player in players)
        {
            if (Debug.isDebugBuild)
                Debug.Log(player.username + " | " + player.kills + " | " + player.deaths);
            GameObject itemGO = Instantiate(playerScoreboardItemPrefab, playerScoreboardList);
            PlayerScoreboardItem item = itemGO.GetComponent<PlayerScoreboardItem>();
            if(item != null)
            {
                item.SetUp(player.username, player.kills, player.deaths);
            }
        }

	}

    private void OnDisable()
    {
        //Clean up item list
        foreach(Transform child in playerScoreboardList)
        {
            Destroy(child.gameObject);
        }
    }
}
