using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconEditorMap : MonoBehaviour {
    [SerializeField]
    public GameObject PlayerUI;
    [SerializeField]
    GameObject PlayerIcon;
    [HideInInspector]
    bool isTheLocalPlayer = false;

    private Vector3 tempIcon;
    // Use this for initialization
    void Start () {
        StartCoroutine(SetUp());
    }
	
	// Update is called once per frame
	void Update () {
        if (isTheLocalPlayer == false)
            return;

        if (PlayerUI.GetComponent<PlayerUI>().isMapOpen)
        {
            PlayerIcon.transform.localScale = new Vector3(5, 5, 5);
        }
        else if (!PlayerUI.GetComponent<PlayerUI>().isMapOpen)
        {
            PlayerIcon.transform.localScale = tempIcon;
        }
    }

    IEnumerator SetUp()
    {
        yield return new WaitForSeconds(5f);

        Player player = GetComponentInParent<Player>();

        isTheLocalPlayer = player.isTheLocalPlayer;
        if (isTheLocalPlayer == false)
            yield break;

        PlayerUI = player.PlayerUI;
        PlayerIcon = this.gameObject;
        tempIcon = PlayerIcon.transform.localScale;
    }
}
