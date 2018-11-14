using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserAccountLobby : MonoBehaviour {

    public Text usernameText;

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
            SceneManager.LoadScene(0);
    }

}
