using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

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
        if (paused)
        {
            GetComponent<RigidbodyFirstPersonController>().enabled = false;
        }
        else
        {
            GetComponent<RigidbodyFirstPersonController>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
            PausedCanvas.SetActive(true);
            //HUD.SetActive(false);
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
            PausedCanvas.SetActive(true);
            //HUD.SetActive(false);
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        //HUD.SetActive(true);
        PausedCanvas.SetActive(false);
    }
    public void InventoryButton()
    {
        Debug.Log("inventory");
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(true);
        OptionsButtonSelected.SetActive(false);
        DisconnectButtonSelected.SetActive(false);
    }
    public void OptionsButton()
    {
        Debug.Log("options");
        ResumeButtonSelected.SetActive(false);
        InventoryButtonSelected.SetActive(false);
        OptionsButtonSelected.SetActive(true);
        DisconnectButtonSelected.SetActive(false);
    }
    public void DisconnectButton()
    {
        Debug.Log("dc");
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
