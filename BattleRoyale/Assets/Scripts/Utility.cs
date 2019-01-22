using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// A collection of some useful utility scripts
/// </summary>
public class Utility {

    /// <summary>
    /// Will set all children of an object to a specified layer, along with the children's children.
    /// </summary>
    /// <param name="_obj"></param>
    /// <param name="_newLayer"></param>
	public static void SetLayerRecursively(GameObject _obj, int _newLayer)
    {
        if (_obj == null)
            return;

        _obj.layer = _newLayer;

        foreach(Transform child in _obj.transform)
        {
            if (child == null)
                continue;
            SetLayerRecursively(child.gameObject, _newLayer);
        }

    }

    /// <summary>
    ///Will take in an object spawned from a Pool and a time, and despawn it after the given time
    ///Similar to Destroy(obj, 2f), but for the Pool
    /// </summary>
    /// <param name="_obj"></param>
    /// <param name="_time"></param>
    /// <returns></returns>
    public static IEnumerator DespawnAfterSeconds(GameObject _obj, float _time)
    {
        yield return new WaitForSeconds(_time);
        SimplePool.Despawn(_obj);
    }

    /// <summary>
    /// Will take the given data string and give you the value wanted, e.g. [KILLS] will return the kill count
    /// </summary>
    /// <param name="_data"></param>
    /// <param name="_valueWanted"></param>
    /// <returns></returns>
    public static int DataToIntValue(string _data, string _valueWanted)
    {
        string[] values = _data.Split('/');
        foreach(string value in values)
        {
            if (value.StartsWith(_valueWanted))
            {
                return int.Parse(value.Substring(_valueWanted.Length));
            }
        }
        Debug.LogError("Utility -- DataToIntValue: " + _valueWanted + " not found in " + _data);
        return 0;
    }

    public static string ValuesToData (int kills, int deaths)
    {
        return UserAccountManager.KillCountDataSymbol + kills + "/" + UserAccountManager.DeathCountDataSymbol + deaths + "/";
    }

    /// <summary>
    /// Instantiates an object on the server, then tells the NetworkServer to spawn it on all clients
    /// </summary>
    /// <param name="_obj"></param>
    /// <param name="_position"></param>
    /// <param name="_rotation"></param>
    public static GameObject InstantiateOverNetwork(GameObject _obj, Vector3 _position, Quaternion _rotation)
    {
        GameObject GO = Object.Instantiate(_obj, _position, _rotation);
        if(NetworkManager.singleton != null)
            NetworkServer.Spawn(GO);
        return GO;
    }

    /// <summary>
    /// Waits for the end of frame before continuing
    /// </summary>
    /// <returns></returns>
    public static IEnumerator WaitForEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Waits for a specified number of seconds before continuing
    /// </summary>
    /// <param name="secondsToWait"></param>
    /// <returns></returns>
    public static IEnumerator WaitForSeconds(float secondsToWait)
    {
        Debug.Log("Waiting " + secondsToWait + " seconds");
        yield return new WaitForSeconds(secondsToWait);
    }

}
