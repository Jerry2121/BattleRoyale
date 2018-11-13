using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    private bool paused;
    [Header("Paused Game Objects")]
    public GameObject PausedCanvas;
    public GameObject HUD;
    public GameObject ResumeButtonSelected;
    public GameObject InventoryButtonSelected;
    public GameObject OptionsButtonSelected;
    public GameObject DisconnectButtonSelected;
    Rigidbody Player;
    public GameObject Player2;
	// Use this for initialization
	void Start () {
        Player = Player2.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Player.constraints = RigidbodyConstraints.FreezePosition;
            paused = true;
            PausedCanvas.SetActive(true);
            HUD.SetActive(false);
            ResumeButtonSelected.SetActive(true);
            InventoryButtonSelected.SetActive(false);
            OptionsButtonSelected.SetActive(false);
            DisconnectButtonSelected.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Resume();
        }
	}
    public void Resume()
    {
        Player.constraints = RigidbodyConstraints.None;
        Player.constraints = RigidbodyConstraints.FreezeRotationX;
        Player.constraints = RigidbodyConstraints.FreezeRotationY;
        Player.constraints = RigidbodyConstraints.FreezeRotationZ;
        paused = false;
        HUD.SetActive(true);
        PausedCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void InventoryButton()
    {
        Player.constraints = RigidbodyConstraints.FreezePosition;
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(true);
        OptionsButtonSelected.SetActive(false);
        DisconnectButtonSelected.SetActive(false);
    }
    public void OptionsButton()
    {
        Player.constraints = RigidbodyConstraints.FreezePosition;
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(false);
        OptionsButtonSelected.SetActive(true);
        DisconnectButtonSelected.SetActive(false);
    }
    public void DisconnectButton()
    {
        Player.constraints = RigidbodyConstraints.FreezePosition;
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(false);
        OptionsButtonSelected.SetActive(false);
        DisconnectButtonSelected.SetActive(true);
    }
    public void YesOption()
    {
        Player.constraints = RigidbodyConstraints.FreezePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }
    public void NoOption()
    {
        Player.constraints = RigidbodyConstraints.FreezePosition;
        Resume();
    }
}
