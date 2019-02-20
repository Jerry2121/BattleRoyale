using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killfeed : MonoBehaviour {

    [SerializeField]
    GameObject killFeedItemPrefab;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitForInstance());
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

    IEnumerator WaitForInstance()
    {
        yield return new WaitForSeconds(5);
        if (GameManager.Instance == null)
            yield return new WaitForSeconds(5);
        if (GameManager.Instance == null)
            throw new System.NullReferenceException("GameManager.Instance is null");

        GameManager.Instance.onPlayerKilledCallback += OnKill;
    }
}
