using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillfeedItem : MonoBehaviour {

    [SerializeField]
    Text killfeedItemText;

    public void SetUp(string _player, string _source)
    {
        killfeedItemText.text = "<b>" + _source + "</b>" + " killed " + "<i>" + _player + "</i>";
    }

}
