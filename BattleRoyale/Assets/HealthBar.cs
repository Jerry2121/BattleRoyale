using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets.Characters.FirstPerson;

public class HealthBar : MonoBehaviour {
    public GameObject Player;
    public GameObject ZoneWall;
    public Slider healthslider;
    public Slider StaminaSlider;
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI StaminaText;
    public float Health = 100;
    public float Stamina = 100;
    public bool run;
    public bool OutsideOfCircle;
    private bool walking;
    private float Timer;
    private KeyCode RunKey = KeyCode.LeftShift;
    private KeyCode WalkForward = KeyCode.W;
    private KeyCode WalkBackward = KeyCode.S;
    private KeyCode WalkLeft = KeyCode.A;
    private KeyCode WalkRight = KeyCode.D;
    // Use this for initialization
    void Start () {
        OutsideOfCircle = false;
        Timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.transform.position.x < ZoneWall.transform.position.x)
        {
            OutsideOfCircle = false;
        }
        else
        {
            OutsideOfCircle = true;
        }
        if (OutsideOfCircle)
        {
            Timer += Time.deltaTime;
           if (Timer >= 5)
            {
                Health -= 10;
                Timer = 0;
            }
        }
        else
        {
            Timer = 0;
        }
        if (Input.GetKey(WalkForward) || Input.GetKey(WalkBackward) || Input.GetKey(WalkLeft) || Input.GetKey(WalkRight))
        {
            walking = true;
        }
        else
        {
            walking = false;
        }
        if (Health >= 100)
        {
            Health = 100;
        }
        else if (Health <= 0)
        {
            Health = 0;
        }
        if (Stamina >= 100)
        {
            Stamina = 100;
        }
        else if (Stamina <= 0)
        {
            run = false;
            Stamina = 0;
        }
        if (Input.GetKey(RunKey) && Stamina > 0 && walking) 
        {
            run = true;
            Stamina -= Time.deltaTime * 3;
        }
        else
        {
            Stamina += Time.deltaTime / 0.75f;
        }
        healthslider.value = Health;
        StaminaSlider.value = Stamina;
        HealthText.GetComponent<TextMeshProUGUI>().text = "+ " + healthslider.value;
        StaminaText.GetComponent<TextMeshProUGUI>().text = "" + StaminaSlider.value;
    }
}
