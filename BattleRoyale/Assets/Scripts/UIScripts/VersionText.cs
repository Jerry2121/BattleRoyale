using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionText : MonoBehaviour {

    TextMeshProUGUI versionText;

	// Use this for initialization
	void Start () {
        versionText = GetComponent<TextMeshProUGUI>();
        if(versionText != null)
            versionText.text = "v" + Application.version;
	}
}
