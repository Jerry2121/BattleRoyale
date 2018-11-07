using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility {

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

    public static void ADespawnAfterSeconds(GameObject _obj, float _time)
    {

    }

    //Will take in a time and and object spawned from a Pool, and despawn it after the given time
    //Similar to Destroy(obj, 2f), but for the Pool
    public static IEnumerator DespawnAfterSeconds(GameObject _obj, float _time)
    {
        yield return new WaitForSeconds(_time);
        SimplePool.Despawn(_obj);
    }

}
