using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool paused;
    [Header("Paused Game Objects")]
    public GameObject PausedCanvas;
    public GameObject HUD;
    public GameObject ResumeButtonSelected;
    public GameObject InventoryButtonSelected;
    public GameObject OptionsButtonSelected;
    public GameObject DisconnectButtonSelected;
    public GameObject Camera;
    Rigidbody Player;
    public GameObject Player2;
    // Use this for initialization
    void Start()
    {
        Player = Player2.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
            Player.constraints = RigidbodyConstraints.FreezeAll;
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
            Resume();
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !paused)
        {
            paused = true;
            Player.constraints = RigidbodyConstraints.FreezeAll;
            PausedCanvas.SetActive(true);
            HUD.SetActive(false);
            ResumeButtonSelected.SetActive(false);
            InventoryButtonSelected.SetActive(true);
            OptionsButtonSelected.SetActive(false);
            DisconnectButtonSelected.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && paused)
        {
            Resume();
        }
    }
    public void Resume()
    {
        Player.constraints = RigidbodyConstraints.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        HUD.SetActive(true);
        PausedCanvas.SetActive(false);
    }
    public void InventoryButton()
    {
       // Player.constraints = RigidbodyConstraints.FreezeAll;
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(true);
        OptionsButtonSelected.SetActive(false);
        DisconnectButtonSelected.SetActive(false);
    }
    public void OptionsButton()
    {
        //Player.constraints = RigidbodyConstraints.FreezeAll;
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(false);
        OptionsButtonSelected.SetActive(true);
        DisconnectButtonSelected.SetActive(false);
    }
    public void DisconnectButton()
    {
        //Player.constraints = RigidbodyConstraints.FreezeAll;
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(false);
        OptionsButtonSelected.SetActive(false);
        DisconnectButtonSelected.SetActive(true);
    }
    public void YesOption()
    {
        //Player.constraints = RigidbodyConstraints.FreezeAll;
        SceneManager.LoadScene("MainMenu");
    }
    public void NoOption()
    {
       // Player.constraints = RigidbodyConstraints.FreezeAll;
        Resume();
    }
}
