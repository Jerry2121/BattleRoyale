using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerScore : MonoBehaviour {

    int lastKills;
    int lastDeaths;

    Player player;

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        StartCoroutine(SyncScoreLoop());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void OnDestroy()
    {
        if(player != null)
            SyncNow();
    }

    IEnumerator SyncScoreLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            SyncNow();
        }
    }

    void SyncNow()
    {
        if (UserAccountManager.IsLoggedIn)
        {
            UserAccountManager.instance.RetrieveData(OnDataReceived);
        }
    }

    void OnDataReceived(string _data)
    {
        if (player.kills <= lastKills && player.deaths <= lastDeaths)
            return;

        int killsSinceLast = player.kills - lastKills;
        int deathsSinceLast = player.deaths - lastDeaths;

        int kills = Utility.DataToIntValue(_data, UserAccountManager.KillCountDataSymbol);
        int deaths = Utility.DataToIntValue(_data, UserAccountManager.DeathCountDataSymbol);

        int newKills = kills + killsSinceLast;
        int newDeaths = deaths + deathsSinceLast;

        lastKills = player.kills;
        lastDeaths = player.deaths;

        string newData = Utility.ValuesToData(newKills, newDeaths);
        Debug.Log("Syncing: " + newData);

        UserAccountManager.instance.SendData(newData);
    }

}
