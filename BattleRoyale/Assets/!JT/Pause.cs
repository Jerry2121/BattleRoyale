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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
            PausedCanvas.SetActive(true);
            HUD.SetActive(false);
            ResumeButtonSelected.SetActive(true);
            InventoryButtonSelected.SetActive(false);
            OptionsButtonSelected.SetActive(false);
            DisconnectButtonSelected.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            paused = false;
            HUD.SetActive(true);
            PausedCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
	}
    public void Resume()
    {
        paused = false;
        HUD.SetActive(true);
        PausedCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void InventoryButton()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(true);
        OptionsButtonSelected.SetActive(false);
        DisconnectButtonSelected.SetActive(false);
    }
    public void OptionsButton()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(false);
        OptionsButtonSelected.SetActive(true);
        DisconnectButtonSelected.SetActive(false);
    }
    public void DisconnectButton()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(false);
        OptionsButtonSelected.SetActive(false);
        DisconnectButtonSelected.SetActive(true);
    }
    public void YesOption()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }
    public void NoOption()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Resume();
    }
}
