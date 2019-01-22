using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killfeed : MonoBehaviour {

    [SerializeField]
    GameObject killFeedItemPrefab;

    // Use this for initialization
    void Start()
    {
        GameManager.Instance.onPlayerKilledCallback += OnKill;
    }

    public void OnKill(string _player, string _source)
    {
        if (Debug.isDebugBuild)
            Debug.Log(_source + " killed " + _player);

        GameObject go  = Instantiate(killFeedItemPrefab, this.transform);
        //go.transform.SetAsFirstSibling(); //Will put it at the top of the list
        go.GetComponent<KillfeedItem>().SetUp(_player, _source);

        Destroy(go, 4f);
    }
}
