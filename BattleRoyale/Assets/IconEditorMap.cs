using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconEditorMap : MonoBehaviour {
    [SerializeField]
    public GameObject PlayerUI;
    [SerializeField]
    GameObject PlayerIcon;

    private Vector3 tempIcon;
    // Use this for initialization
    void Start () {
        PlayerUI = GetComponentInParent<Player>().PlayerUI;
        PlayerIcon = this.gameObject;
        tempIcon = PlayerIcon.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerUI.GetComponent<PlayerUI>().isMapOpen)
        {
            PlayerIcon.transform.localScale = new Vector3(5, 5, 5);
        }
        else if (!PlayerUI.GetComponent<PlayerUI>().isMapOpen)
        {
            PlayerIcon.transform.localScale = tempIcon;
        }
    }
}
