using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    public bool canspawnItem;
    private int d1;
	// Use this for initialization
	void Start () {
        canspawnItem = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (canspawnItem)
        {
            d1 = Random.Range(0, 7);
            if (d1 == 1)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().ItemSpawn1, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
            if (d1 == 2)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().ItemSpawn2, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
            if (d1 == 3)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().ItemSpawn3, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
            if (d1 == 4)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().ItemSpawn4, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
            if (d1 == 5)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().ItemSpawn5, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
            if (d1 == 6)
            {
                Instantiate(GameObject.Find("GameManager").GetComponent<GameManagerScript>().ItemSpawn6, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                canspawnItem = false;
            }
        }
	}
}
