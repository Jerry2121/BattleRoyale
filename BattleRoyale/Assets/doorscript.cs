using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class doorscript : MonoBehaviour {
    public GameObject Canvas;
    public bool active;
    public Animator animator;
    public TextMeshProUGUI text;
    public bool open;
	// Use this for initialization
	void Start () {
        active = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!open && active)
        {
            text.text = "Press E to open.";
        }
        else if (open && active)
        {
            text.text = "Press E to close.";
        }
        if (Input.GetKeyDown(KeyCode.E) && active && !open)
        {
            animator.SetBool("DoorOpen", true);
            open = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && active && open)
        {
            animator.SetBool("DoorOpen", false);
            open = false;
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        active = true;
        Canvas.SetActive(true);
    }
    public void OnTriggerExit(Collider other)
    {
        active = false;
        Canvas.SetActive(false);
    }
}
