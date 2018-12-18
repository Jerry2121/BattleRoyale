using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIOptions : MonoBehaviour {

    public GameObject gamePanel;
    public GameObject gameLines;
    public GameObject controlsPanel;
    public GameObject controlLines;
    public GameObject videoPanel;
    public GameObject videoLines;

    public void ActivateGame()
    {
        gamePanel.SetActive(true);
        gameLines.SetActive(true);
        controlsPanel.SetActive(false);
        controlLines.SetActive(false);
        videoPanel.SetActive(false);
        videoLines.SetActive(false);
    }

    public void ActivateControls()
    {
        gamePanel.SetActive(false);
        gameLines.SetActive(false);
        controlsPanel.SetActive(true);
        controlLines.SetActive(true);
        videoPanel.SetActive(false);
        videoLines.SetActive(false);
    }

    public void ActivateVideo()
    {
        gamePanel.SetActive(false);
        gameLines.SetActive(false);
        controlsPanel.SetActive(false);
        controlLines.SetActive(false);
        videoPanel.SetActive(true);
        videoLines.SetActive(true);
    }

}
