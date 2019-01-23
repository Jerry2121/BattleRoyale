using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameplate : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI usernameText;
    [SerializeField]
    RectTransform healthBarFill;
    [SerializeField]
    Player player;
    [SerializeField]
    GameObject DevText;

    // Update is called once per frame
    void Update() {
        Camera cam = Camera.main;

        if (player.username != "Loading..." && usernameText.text != player.username)
            usernameText.text = player.username;
        healthBarFill.localScale = new Vector3(player.GetHealthPercentage(), 1f, 1f);
        if (player.username == "Jerry2121" || player.username == "Aaron")
        {
            DevText.SetActive(true);
        }
        else
        {
            DevText.SetActive(false);
        }
        //Have canvas always face away from camera
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);

	}
}
