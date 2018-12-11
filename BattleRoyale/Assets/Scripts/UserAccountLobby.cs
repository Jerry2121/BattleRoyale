using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UserAccountLobby : MonoBehaviour {

    public TextMeshProUGUI usernameText;

	// Use this for initialization
	void Start () {
        if (UserAccountManager.IsLoggedIn)
            usernameText.text = "Logged In As: " + UserAccountManager.PlayerUsername;
        else
            usernameText.text = "Logged In As: Game Dev";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LogOut()
    {
        if (UserAccountManager.IsLoggedIn)
            UserAccountManager.instance.LogOut();
        else
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel(0);
    }

}
