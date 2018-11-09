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
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            paused = false;
            HUD.SetActive(true);
            PausedCanvas.SetActive(false);
        }
	}
    public void Resume()
    {
        paused = false;
        HUD.SetActive(true);
        PausedCanvas.SetActive(false);
    }
    public void InventoryButton()
    {
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(true);
        OptionsButtonSelected.SetActive(false);
        DisconnectButtonSelected.SetActive(false);
    }
    public void OptionsButton()
    {
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(false);
        OptionsButtonSelected.SetActive(true);
        DisconnectButtonSelected.SetActive(false);
    }
    public void DisconnectButton()
    {
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(false);
        OptionsButtonSelected.SetActive(false);
        DisconnectButtonSelected.SetActive(true);
    }
    public void YesOption()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void NoOption()
    {
        Resume();
    }
}
