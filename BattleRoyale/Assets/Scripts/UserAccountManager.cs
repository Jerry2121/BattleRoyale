using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DatabaseControl;
using UnityEngine.SceneManagement;

public class UserAccountManager : MonoBehaviour {

    public static UserAccountManager instance;

    //These store the username and password of the player when they have logged in
    public static string PlayerUsername { get; protected set; }
    private static string PlayerPassword = "";

    public static bool IsLoggedIn { get; protected set; }

    public string loggedInSceneName = "MainMenu";
    public string logInMenuSceneName = "LoginScene";

    public static string KillCountDataSymbol = "[KILLS]";
    public static string DeathCountDataSymbol = "[DEATHS]";

    public delegate void OnDataReceivedCallback(string _data);

    // Use this for initialization
    void Awake () {
        if (instance != null)
        {
            if (Debug.isDebugBuild)
                Debug.LogWarning("UseAccountManager -- Awake: There is more than one UserAccountManager in the scene. Only one will be set to UserAccountManager.instance, and others will be destroyed.");
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LogOut()
    {
        PlayerUsername = "";
        PlayerPassword = "";
        IsLoggedIn = false;

        if (Debug.isDebugBuild)
            Debug.Log("User " + PlayerUsername + " has logged out");

        SceneManager.LoadScene(logInMenuSceneName);
    }

    public void LogIn(string _username, string _password)
    {
        PlayerUsername = _username;
        PlayerPassword = _password;

        IsLoggedIn = true;

        if (Debug.isDebugBuild)
            Debug.Log("User " + PlayerUsername + " has logged in");

        GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToLevel(1);
    }

    IEnumerator GetData(OnDataReceivedCallback _onDataReceivedCallback)
    {
        IEnumerator e = DCF.GetUserData(PlayerUsername, PlayerPassword); // << Send request to get the player's data string. Provides the username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request
        if (response == "Error")
        {
            //There was another error. Automatically logs player out. This error message should never appear, but is here just in case.

            UserAccountManager.instance.LogOut();

            /*loginParent.gameObject.SetActive(true);
            loadingParent.gameObject.SetActive(false);
            Login_ErrorText.text = "Error: Unknown Error. Please try again later.";*/
        }
        else
        {
            //The player's data was retrieved. Goes back to loggedIn UI and displays the retrieved data in the InputField
            if(_onDataReceivedCallback != null)
                _onDataReceivedCallback.Invoke(response);
        }
    }
    IEnumerator SetData(string data)
    {
        IEnumerator e = DCF.SetUserData(PlayerUsername, PlayerPassword, data); // << Send request to set the player's data string. Provides the username, password and new data string
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
        {
            //The data string was set correctly. Goes back to LoggedIn UI
        }
        else
        {
            //There was another error. Automatically logs player out. This error message should never appear, but is here just in case.

            UserAccountManager.instance.LogOut();

            /*loginParent.gameObject.SetActive(true);
            loadingParent.gameObject.SetActive(false);
            Login_ErrorText.text = "Error: Unknown Error. Please try again later.";*/
        }
    }

    public void SendData(string _data)
    {
        if (IsLoggedIn)
        {
            //Called when the player hits 'Set Data' to change the data string on their account. Switches UI to 'Loading...' and starts coroutine to set the players data string on the server
            StartCoroutine(SetData(_data));
        }
    }
    public void RetrieveData(OnDataReceivedCallback _onDataReceivedCallback)
    {
        if(IsLoggedIn){
            //Called when the player hits 'Get Data' to retrieve the data string on their account. Switches UI to 'Loading...' and starts coroutine to get the players data string from the server
            StartCoroutine(GetData(_onDataReceivedCallback));
        }
    }

}
