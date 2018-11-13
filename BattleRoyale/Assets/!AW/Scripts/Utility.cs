using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
