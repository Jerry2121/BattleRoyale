using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNews : MonoBehaviour {

    [SerializeField]
    GameObject newsGameObject;

    public void ShowNews()
    {
        newsGameObject.SetActive(!newsGameObject.activeSelf);
    }

}
