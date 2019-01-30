using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNews : MonoBehaviour {

    [SerializeField]
    GameObject newsText;

    public void ShowNews()
    {
        newsText.SetActive(!newsText.activeSelf);
    }

}
